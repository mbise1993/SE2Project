using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using HopeHouse.Common.EventFramework;
using HopeHouse.Common.Helpers;
using HopeHouse.Common.Util;
using HopeHouse.Presentation.Commanding;

namespace HopeHouse.Presentation.ViewModels
{
    public class DatabaseBackupSettingsViewModel : ViewModelBase
    {
        #region Private Fields

        private int _selectedBackupOption;
        private string _backupPath;

        #endregion

        #region Properties

        public string LastBackupDate
        {
            get
            {
                return SettingsManager.GetLastBackupDate();
            }
        }

        public int SelectedBackupOption
        {
            set
            {
                _selectedBackupOption = value;
                OnPropertyChanged(nameof(IsBackupSelected));
            }
        }

        public bool IsBackupSelected
        {
            get
            {
                return _selectedBackupOption == 0 || _selectedBackupOption == 1;
            }
        }

        public string BackupPath
        {
            get
            {
                return _backupPath;
            }
            private set
            {
                _backupPath = value;
                OnPropertyChanged(nameof(BackupPath));
            }
        }

        #endregion

        #region Commands

        private ICommand _selectDirectoryCommand;
        public ICommand SelectDirectoryCommand
        {
            get
            {
                if (_selectDirectoryCommand == null)
                {
                    _selectDirectoryCommand = new RelayCommand(arg =>
                    {
                        FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                        folderBrowserDialog.Description = "Select Backup Path";
                        folderBrowserDialog.ShowNewFolderButton = true;

                        if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                        {
                            BackupPath = folderBrowserDialog.SelectedPath;
                        }
                    },
                    arg => true);
                }

                return _selectDirectoryCommand;
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
                        switch (_selectedBackupOption)
                        {
                            case 0:
                                SettingsManager.SetBackupReminder("AutomaticBackupsOn");
                                SettingsManager.SetAutoBackupPath(_backupPath);
                                BackupHelper.PerformAutomaticDatabaseBackup();
                                break;

                            case 1:
                                SettingsManager.SetBackupReminder((DateTime.Now + new TimeSpan(7, 0, 0, 0)).ToShortDateString());
                                BackupHelper.BackupDatabase(_backupPath);
                                break;

                            case 2:
                                SettingsManager.SetBackupReminder("NextRun");
                                break;

                            case 3:
                                SettingsManager.SetBackupReminder((DateTime.Now + new TimeSpan(7, 0, 0, 0)).ToShortDateString());
                                break;

                            case 4:
                                SettingsManager.SetBackupReminder((DateTime.Now + new TimeSpan(30, 0, 0, 0)).ToShortDateString());
                                break;
                        }

                        Window suggestBackupDialog = EventProvider.PublishGetWindow("DatabaseBackupSettingsWindow");
                        if (suggestBackupDialog != null)
                        {
                            suggestBackupDialog.DialogResult = true;
                        }
                    },
                    arg => !IsBackupSelected || !string.IsNullOrEmpty(_backupPath));
                }

                return _okCommand;
            }
        }

        #endregion
    }
}
