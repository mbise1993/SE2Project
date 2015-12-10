using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HopeHouse.Core.Models;
using System.ComponentModel;
using HopeHouse.Core.DataAccess;
using System.IO;
using System.Linq;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using HopeHouse.Presentation.Commanding;
using HopeHouse.Common.EventFramework;
using HopeHouse.Common.Helpers;
using HopeHouse.Common.Logging;
using HopeHouse.Common.Util;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System.Windows;

namespace HopeHouse.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Private Fields

        private bool _isClientOpInProgress;
        private bool _isImportInProgress;
        private int _clientsImported;
        private ICollection<Client> _clients; 
        private Staff _loggedInUser;
        private Client _selectedClient;
        private FilterManager _filterManager;
        private string _selectedFilterTypeString;
        private string _selectedFilterFieldString;
        private string _filterString;
        private IList<string> _filterDayOptions;
        private string _filterDay;
        private IList<string> _filterMonthOptions; 
        private string _filterMonth;
        private IList<string> _filterYearOptions; 
        private string _filterYear;

        private static object _syncLock = new object();

        #endregion

        #region Properties

        public bool AreClientsLoaded
        {
            get
            {
                return ClientManager.AreClientsLoaded();
            }
        }

        public int NumClientsInSystem
        {
            get
            {
                return ClientManager.GetNumberOfClients();
            }
        }

        public bool IsClientOpInProgress
        {
            get
            {
                return _isClientOpInProgress;
            }
            private set
            {
                _isClientOpInProgress = value;
                OnPropertyChanged(nameof(IsClientOpInProgress));
            }
        }

        public bool IsImportInProgress
        {
            get
            {
                return _isImportInProgress;
            }
            private set
            {
                _isImportInProgress = value;
                OnPropertyChanged(nameof(IsImportInProgress));
            }
        }

        public int ClientsImported
        {
            get
            {
                return _clientsImported;
            }
            private set
            {
                _clientsImported = value;
                OnPropertyChanged(nameof(ClientsImported));
            }
        }

        public bool IsClientListEmpty
        {
            get
            {
                return Clients.Count == 0;
            }
        }

        public Staff LoggedInUser
        {
            get
            {
                return _loggedInUser;
            }
            set
            {
                _loggedInUser = value;
                OnPropertyChanged(nameof(LoggedInUser));
            }
        }

        public ICollection<Client> Clients
        {
            get
            {
                if (_clients == null)
                {
                    _clients = new ObservableCollection<Client>();
                }

                return _clients;
            }
            set
            {
                if (value == null)
                {
                    _clients.Clear();
                }
                else
                {
                    _clients = value;
                }

                OnPropertyChanged(nameof(Clients));
            }
        }

        public Client SelectedClient
        {
            get
            {
                return _selectedClient;
            }
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public ICollection<string> FilterTypeOptions
        {
            get
            {
                return _filterManager.GetFilterTypeOptions();
            }
        }

        public string SelectedFilterTypeString
        {
            get
            {
                return _selectedFilterTypeString;
            }
            set
            {
                _selectedFilterTypeString = value;
                OnPropertyChanged(nameof(SelectedFilterTypeString));
                OnPropertyChanged(nameof(FilterFieldOptions));
            }
        }

        public ICollection<string> FilterFieldOptions
        {
            get
            {
                return _filterManager.GetFilterFieldOptions(_selectedFilterTypeString);
            }
        }

        public string SelectedFilterFieldString
        {
            get
            {
                return _selectedFilterFieldString;
            }
            set
            {
                _selectedFilterFieldString = value;
                OnPropertyChanged(nameof(SelectedFilterFieldString));
                OnPropertyChanged(nameof(IsBooleanFieldSelected));
                OnPropertyChanged(nameof(IsDateFieldSelected));
            }
        }

        public bool IsBooleanFieldSelected
        {
            get
            {
                if (!string.IsNullOrEmpty(_selectedFilterTypeString) &&
                    !string.IsNullOrEmpty(_selectedFilterFieldString))
                {
                    return _filterManager.IsPropertyType<bool>(_selectedFilterTypeString, _selectedFilterFieldString);
                }

                return false;
            }
        }

        public bool IsDateFieldSelected
        {
            get
            {
                if (!string.IsNullOrEmpty(_selectedFilterTypeString) &&
                    !string.IsNullOrEmpty(_selectedFilterFieldString))
                {
                    return _filterManager.IsPropertyType<DateTime>(_selectedFilterTypeString, _selectedFilterFieldString);
                }

                return false;
            }
        }

        public string FilterString
        {
            get
            {
                return _filterString;
            }
            set
            {
                _filterString = value;
                OnPropertyChanged(nameof(FilterString));
            }
        }

        public IList<string> FilterDayOptions
        {
            get
            {
                return _filterDayOptions;
            }
        }

        public string FilterDay
        {
            get
            {
                return _filterDay;
            }
            set
            {
                _filterDay = value;
                OnPropertyChanged(nameof(FilterDay));
            }
        }

        public IList<string> FilterMonthOptions
        {
            get
            {
                return _filterMonthOptions;
            }
        }

        public string FilterMonth
        {
            get
            {
                return _filterMonth;
            }
            set
            {
                _filterMonth = value;
                OnPropertyChanged(nameof(FilterMonth));
            }
        }

        public IList<string> FilterYearOptions
        {
            get
            {
                return _filterYearOptions;
            }
        }

        public string FilterYear
        {
            get
            {
                return _filterYear;
            }
            set
            {
                _filterYear = value;
                OnPropertyChanged(nameof(FilterYear));
            }
        }

        public ICollection<Filter> AppliedFilters
        {
            get
            {
                return _filterManager.AppliedFilters;
            }
        }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            _clients = new ObservableCollection<Client>();
            _filterManager = new FilterManager();
            _filterDayOptions = new List<string>();
            _filterMonthOptions = new List<string>();
            _filterYearOptions = new List<string>();

            BindingOperations.EnableCollectionSynchronization(Clients, _syncLock);

            CheckClientsLoaded();
            PopulateFilterDayOptions();
            PopulateFilterMonthOptions();
            PopulateFilterYearOptions();

            ClientManager.ClientAdded += OnClientAdded;
            ClientManager.ClientDeleted += OnClientDeleted;
        }

        #endregion

        #region Commands

        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(arg =>
                    {
                        Application.Current.Shutdown(Environment.ExitCode);
                    },
                    arg => true);
                }

                return _exitCommand;
            }
        }

        private ICommand _backupNowCommand;
        public ICommand BackupNowCommand
        {
            get
            {
                if (_backupNowCommand == null)
                {
                    _backupNowCommand = new RelayCommand(arg =>
                    {
                        string backupPath = string.Empty;

                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.Description = "Select Backup Path";
                        folderBrowserDialog.ShowNewFolderButton = true;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            backupPath = folderBrowserDialog.SelectedPath;
                        }

                        BackupHelper.BackupDatabase(backupPath);
                    },
                    arg => true);
                }

                return _backupNowCommand;
            }
        }

        private ICommand _changeBackupSettingsCommand;
        public ICommand ChangeBackupSettingsCommand
        {
            get
            {
                if (_changeBackupSettingsCommand == null)
                {
                    _changeBackupSettingsCommand = new RelayCommand(arg =>
                    {
                        EventProvider.PublishShowDialog("DatabaseBackupSettingsWindow");
                    },
                    arg => true);
                }

                return _changeBackupSettingsCommand;
            }
        }

        private ICommand _addStaffMemberCommand;
        public ICommand AddStaffMemberCommand
        {
            get
            {
                if(_addStaffMemberCommand == null)
                {
                    _addStaffMemberCommand = new RelayCommand(arg =>
                    {
                        EventProvider.PublishShowDialog("AddStaffMemberWindow");

                    },
                    arg => true);
                }

                return _addStaffMemberCommand;
            }
        }

        private ICommand _viewStaffMembersCommand;
        public ICommand ViewStaffMembersCommand
        {
            get
            {
                if (_viewStaffMembersCommand == null)
                {
                    _viewStaffMembersCommand = new RelayCommand(arg =>
                    {
                        EventProvider.PublishViewStaffMembersDisplay(_loggedInUser);
                    },
                    arg => true);
                }

                return _viewStaffMembersCommand;
            }
        }

        private ICommand _viewUserActionLogCommand;
        public ICommand ViewUserActionLogCommand
        {
            get
            {
                if (_viewUserActionLogCommand == null)
                {
                    _viewUserActionLogCommand = new RelayCommand(arg =>
                    {
                        EventProvider.PublishViewUserActionLogDisplay();
                    },
                    arg => true);
                }

                return _viewUserActionLogCommand;
            }
        }

        private ICommand _clearUserActionLogCommand;
        public ICommand ClearUserActionLogCommand
        {
            get
            {
                if (_clearUserActionLogCommand == null)
                {
                    _clearUserActionLogCommand = new RelayCommand(arg =>
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to clear the contents of the current log file?",
                            "Confirm Clear", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            Logger.DeleteLogFile();

                            UserActionLogEntry logEntry = new UserActionLogEntry(_loggedInUser.ToString(), _loggedInUser.Username,
                                "Cleared log file");
                            Logger.WriteUserActionLogEntry(logEntry);
                        }
                    },
                    arg => true);
                }

                return _clearUserActionLogCommand;
            }
        }

        private ICommand _importClientsCommand;
        public ICommand ImportClientsCommand
        {
            get
            {
                if (_importClientsCommand == null)
                {
                    _importClientsCommand = new RelayCommand(arg =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title = "Open Excel file for import";
                        openFileDialog.Filter = "Excel files (*.xls, *.xlsx)|*.xls; *.xlsx";

                        string filePath = null;
                        if (openFileDialog.ShowDialog() ==  true)
                        {
                            filePath = openFileDialog.FileName;
                        }

                        while(IsFileInUse(filePath))
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show(
                                $"File {filePath} is currently in use. Please close any programs that may be using the file" +
                                " to continue with the import", "File In Use", MessageBoxButton.OKCancel, MessageBoxImage.Error);

                            if(result == MessageBoxResult.Cancel)
                            {
                                return;
                            }
                        }

                        if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                        {
                            BackgroundWorker importWorker = new BackgroundWorker();
                            importWorker.WorkerSupportsCancellation = true;

                            importWorker.DoWork += (sender, args) =>
                            {
                                try
                                {
                                    ClientManager.ImportClients(filePath, (x => ClientsImported = x));
                                }
                                catch(InvalidOperationException e)
                                {
                                    args.Result = e;
                                }
                            };

                            importWorker.RunWorkerCompleted += (sender, args) =>
                            {
                                OnPropertyChanged(nameof(NumClientsInSystem));
                                IsImportInProgress = false;

                                if(args.Result is InvalidOperationException)
                                {
                                    System.Windows.MessageBox.Show($"Unable to import Excel file '{Path.GetFileName(filePath)}'.\n" +
                                        "You must have Excel installed in order to use the import feature.", "Error",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            };

                            IsImportInProgress = true;

                            WorkerThreadManager.AddWorkerThread(importWorker);
                            importWorker.RunWorkerAsync();
                        }
                    },
                    arg => true);
                }

                return _importClientsCommand;
            }
        }

        private ICommand _addNewClientCommand;
        public ICommand AddNewClientCommand
        {
            get
            {
                if (_addNewClientCommand == null)
                {
                    _addNewClientCommand = new RelayCommand(arg =>
                    {
                        EventProvider.PublishAddNewClientDisplay(_loggedInUser);
                    },
                    arg => true);
                }

                return _addNewClientCommand;
            }
        }

        private ICommand _presetFilterCommand;
        public ICommand PresetFilterCommand
        {
            get
            {
                if (_presetFilterCommand == null)
                {
                    _presetFilterCommand = new RelayCommand(arg =>
                    {
                        if (string.IsNullOrEmpty((string) arg))
                        {
                            return;
                        }

                        BackgroundWorker filterClientsWorker = new BackgroundWorker();
                        filterClientsWorker.DoWork += (sender, e) =>
                        {
                            if ((string) arg == "All")
                            {
                                SelectedFilterTypeString = "Client";
                                SelectedFilterFieldString = "First Name";
                                FilterString = string.Empty;
                                FilterDay = string.Empty;
                                FilterMonth = string.Empty;
                                FilterYear = string.Empty;

                                e.Result = _filterManager.GetAllClients();
                            }

                            if ((string)arg == "CurrentlyActive")
                            {
                                SelectedFilterTypeString = "Client";
                                SelectedFilterFieldString = "Is Active?";
                                FilterString = "True";

                                e.Result = _filterManager.GetPresetFilteredClients(new ActiveClientsFilter());
                            }

                            if ((string)arg == "AddedThisYear")
                            {
                                SelectedFilterTypeString = "Client";
                                SelectedFilterFieldString = "Date Entered";
                                FilterDay = "Any";
                                FilterMonth = "Any";
                                FilterYear = DateTime.Now.Year.ToString();

                                e.Result = _filterManager.GetPresetFilteredClients(new AddedThisYearFilter());
                            }
                        };

                        filterClientsWorker.RunWorkerCompleted += (sender, e) =>
                        {
                            if (e.Result != null && e.Result is ICollection<Client>)
                            {
                                Clients = (ICollection<Client>) e.Result;

                                if ((string) arg == "All")
                                {
                                    OnPropertyChanged(nameof(AppliedFilters));
                                }

                                OnPropertyChanged(nameof(IsClientListEmpty));
                                IsClientOpInProgress = false;
                            }
                        };

                        IsClientOpInProgress = true;
                        filterClientsWorker.RunWorkerAsync();
                    },
                    arg => AreClientsLoaded);
                }

                return _presetFilterCommand;
            }
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(arg =>
                    {
                        BackgroundWorker filterClientsWorker = new BackgroundWorker();

                        filterClientsWorker.DoWork += (sender, e) =>
                        {
                            if (IsDateFieldSelected)
                            {
                                _filterManager.GetDateFilteredClients(_selectedFilterTypeString, _selectedFilterFieldString, 
                                    _filterDay, _filterMonth, _filterYear);
                            }
                            else
                            {
                                e.Result = _filterManager.GetStringFilteredClients(_selectedFilterTypeString, _selectedFilterFieldString, 
                                    _filterString);
                            }
                        };

                        filterClientsWorker.RunWorkerCompleted += (sender, e) =>
                        {
                            if (e.Result != null && e.Result is ICollection<Client>)
                            {
                                Clients = (ICollection<Client>) e.Result;

                                OnPropertyChanged(nameof(IsClientListEmpty));
                                IsClientOpInProgress = false;
                            }
                        };

                        IsClientOpInProgress = true;
                        filterClientsWorker.RunWorkerAsync();
                    },
                    arg => (AreClientsLoaded) &&
                           (!string.IsNullOrEmpty(_selectedFilterTypeString)) &&
                           (!string.IsNullOrEmpty(_selectedFilterFieldString)) &&
                           ((!string.IsNullOrEmpty(_filterString)) ||
                           ((!string.IsNullOrEmpty(_filterDay) &&
                           (!string.IsNullOrEmpty(_filterMonth) &&
                           (!string.IsNullOrEmpty(_filterYear)))))));
                }

                return _searchCommand;
            }
        }

        private ICommand _addFilterCommand;
        public ICommand AddFilterCommand
        {
            get
            {
                if (_addFilterCommand == null)
                {
                    _addFilterCommand = new RelayCommand(arg =>
                    {
                        IsClientOpInProgress = true;

                        BackgroundWorker addFilterWorker = new BackgroundWorker();
                        addFilterWorker.DoWork += (sender, e) =>
                        {
                            _filterManager.AddAppliedFilter();
                        };

                        addFilterWorker.RunWorkerCompleted += (sender, e) =>
                        {
                            OnPropertyChanged(nameof(AppliedFilters));
                            IsClientOpInProgress = false;

                            FilterString = string.Empty;
                            FilterDay = "Any";
                            FilterMonth = "Any";
                            FilterYear = "Any";
                        };

                        addFilterWorker.RunWorkerAsync();
                    },
                    arg => (_filterManager.CurrentFilter != null));
                }

                return _addFilterCommand;
            }
        }

        private ICommand _deleteFilterCommand;
        public ICommand DeleteFilterCommand
        {
            get
            {
                if (_deleteFilterCommand == null)
                {
                    _deleteFilterCommand = new RelayCommand(arg =>
                    {
                        if (!(arg is Filter))
                        {
                            return;
                        }

                        IsClientOpInProgress = true;

                        BackgroundWorker deleteFilterWorker = new BackgroundWorker();
                        deleteFilterWorker.DoWork += (sender, e) =>
                        {
                            e.Result = _filterManager.DeleteAppliedFilter((Filter)arg);
                        };

                        deleteFilterWorker.RunWorkerCompleted += (sender, e) =>
                        {
                            if (e.Result != null && e.Result is ICollection<Client>)
                            {
                                Clients = (ICollection<Client>) e.Result;

                                OnPropertyChanged(nameof(AppliedFilters));
                                IsClientOpInProgress = false;
                            }
                        };

                        deleteFilterWorker.RunWorkerAsync();
                    },
                    arg => true);
                }

                return _deleteFilterCommand;
            }
        }

        private ICommand _deleteClientCommand;
        public ICommand DeleteClientCommand
        {
            get
            {
                if (_deleteClientCommand == null)
                {
                    _deleteClientCommand = new RelayCommand(arg =>
                    {
                        if (arg is Client)
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show($"Are you sure you want to delete staff member {(arg as Client).GetIdentifier()}?",
                                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                            if (result == MessageBoxResult.Yes)
                            {
                                ClientManager.DeleteClient((Client)arg);

                                UserActionLogEntry logEntry = new UserActionLogEntry(_loggedInUser.ToString(), _loggedInUser.Username,
                                    $"Deleted client {(arg as Client).ToString()}");
                                Logger.WriteUserActionLogEntry(logEntry);
                            }
                        }
                    },
                    arg => true);
                }

                return _deleteClientCommand;
            }
        }

        #endregion

        #region Private Methods

        private void CheckClientsLoaded()
        {
            if (!ClientManager.AreClientsLoaded())
            {
                ClientManager.ClientsLoaded += () =>
                {
                    OnPropertyChanged(nameof(AreClientsLoaded));
                    OnPropertyChanged(nameof(NumClientsInSystem));
                };
            }
        }

        private void OnClientAdded(Client client)
        {
            if (Clients != null && _filterManager.CurrentFilter != null)
            {
                if (_filterManager.CurrentFilter.CheckFilter(client))
                {
                    Clients.Add(client);
                    OnPropertyChanged(nameof(Clients));
                    OnPropertyChanged(nameof(NumClientsInSystem));
                }
            }
        }

        private void OnClientDeleted(Client client)
        {
            if(Clients != null && Clients.Contains(client))
            {
                Clients.Remove(client);
                OnPropertyChanged(nameof(Clients));
                OnPropertyChanged(nameof(NumClientsInSystem));
            }
        }

        private void PopulateFilterDayOptions()
        {
            IEnumerable<int> filterDayInts = Enumerable.Range(1, 31);
            _filterDayOptions.Add("Any");

            foreach (int dayInt in filterDayInts)
            {
                _filterDayOptions.Add(dayInt.ToString());
            }
        }

        private void PopulateFilterMonthOptions()
        {
            IEnumerable<int> filterMonthInts = Enumerable.Range(1, 12);
            _filterMonthOptions.Add("Any");

            foreach (int monthInt in filterMonthInts)
            {
                _filterMonthOptions.Add(monthInt.ToString());
            }
        }

        private void PopulateFilterYearOptions()
        {
            IEnumerable<int> filterYearInts = Enumerable.Range(1920, (DateTime.Now.Year - 1920) + 1);

            foreach (int yearInt in filterYearInts)
            {
                _filterYearOptions.Add(yearInt.ToString());
            }

            _filterYearOptions = _filterYearOptions.Reverse().ToList();
            _filterYearOptions.Insert(0, "Any");
        }

        private bool IsFileInUse(string fileName)
        {
            if(!string.IsNullOrEmpty(fileName))
            {
                FileStream fileStream = null;

                try
                {
                    FileInfo file = new FileInfo(fileName);
                    fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                }
                catch(IOException)
                {
                    return true;
                }
                finally
                {
                    if(fileStream != null)
                    {
                        fileStream.Close();
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
