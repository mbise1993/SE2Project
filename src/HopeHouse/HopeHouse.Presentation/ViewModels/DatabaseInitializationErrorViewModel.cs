using System.Windows;
using System.Windows.Input;
using HopeHouse.Common.EventFramework;
using HopeHouse.Common.Helpers;
using HopeHouse.Common.Util;
using HopeHouse.Presentation.Commanding;
using Microsoft.Win32;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Presentation.ViewModels
{
    public class DatabaseInitializationErrorViewModel : ViewModelBase
    {
        #region Private Fields

        private int _selection;
        private string _backupDatabasePath;

        #endregion

        #region Properties

        public int Selection
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                OnPropertyChanged(nameof(Selection));
                OnPropertyChanged(nameof(IsLoadBackupSelected));
            }
        }

        public bool IsLoadBackupSelected
        {
            get
            {
                return _selection == 0;
            }
        }

        public string BackupDatabasePath
        {
            get
            {
                return _backupDatabasePath;
            }
            private set
            {
                _backupDatabasePath = value;
                OnPropertyChanged(nameof(BackupDatabasePath));
            }
        }

        #endregion

        #region Commands

        private ICommand _selectFileCommand;
        public ICommand SelectFileCommand
        {
            get
            {
                if (_selectFileCommand == null)
                {
                    _selectFileCommand = new RelayCommand(arg =>
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title = "Select Database File";
                        openFileDialog.Filter = "Database files (*.sqlite)|*.sqlite|All files (*.*)|*.*";

                        if (openFileDialog.ShowDialog() == true)
                        {
                            BackupDatabasePath = openFileDialog.FileName;
                        }
                    },
                    arg => true);
                }

                return _selectFileCommand;
            }
        }

        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(arg =>
                    {
                        if (_selection == 0)
                        {
                            BackupHelper.UseBackupDatabase(_backupDatabasePath);
                        }

                        if (_selection == 1)
                        {
                            Database.CreateDatabase(SettingsManager.GetDatabasePath());
                        }

                        Window errorDialog = EventProvider.PublishGetWindow("DatabaseInitializationErrorWindow");
                        if (errorDialog != null)
                        {
                            errorDialog.DialogResult = true;
                        }
                    },
                    arg => (_selection == 0 && !string.IsNullOrEmpty(_backupDatabasePath)) || 
                           (_selection == 1));
                }

                return _okCommand;
            }
        }

        #endregion
    }
}
