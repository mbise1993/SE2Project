
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using HopeHouse.Common.EventFramework;
using HopeHouse.Core.DataAccess;
using HopeHouse.Core.Models;
using HopeHouse.Presentation.Commanding;
using System.Windows;
using HopeHouse.Common.Logging;

namespace HopeHouse.Presentation.ViewModels
{
    public class ViewStaffMembersViewModel : ViewModelBase
    {
        #region Private Fields

        private Staff _loggedInUser;
        private ICollection<Staff> _staffMembers;

        #endregion

        #region Properties

        public ICollection<Staff> StaffMembers
        {
            get
            {
                if (_staffMembers == null)
                {
                    _staffMembers = new ObservableCollection<Staff>();
                }

                return _staffMembers;
            }
        }

        #endregion

        #region Constructor

        public ViewStaffMembersViewModel(Staff loggedInUser)
        {
            _loggedInUser = loggedInUser;
            _staffMembers = new ObservableCollection<Staff>(StaffDatabaseHelper.GetAllStaff());
        }

        #endregion

        #region Commands

        private ICommand _addStaffMemberCommand;
        public ICommand AddStaffMemberCommand
        {
            get
            {
                if (_addStaffMemberCommand == null)
                {
                    _addStaffMemberCommand = new RelayCommand(arg =>
                    {
                        bool? dialogResult = EventProvider.PublishShowDialog("AddStaffMemberWindow");

                        if (dialogResult != null && dialogResult.Value)
                        {
                            _staffMembers = new ObservableCollection<Staff>(StaffDatabaseHelper.GetAllStaff());
                            OnPropertyChanged(nameof(StaffMembers));

                            UserActionLogEntry logEntry = new UserActionLogEntry(_loggedInUser.ToString(), _loggedInUser.Username,
                                "Added a staff member");
                            Logger.WriteUserActionLogEntry(logEntry);
                        }
                    },
                    arg => true);
                }

                return _addStaffMemberCommand;
            }
        }

        private ICommand _deleteStaffMemberCommand;
        public ICommand DeleteStaffMemberCommand
        {
            get
            {
                if (_deleteStaffMemberCommand == null)
                {
                    _deleteStaffMemberCommand = new RelayCommand(arg =>
                    {
                        if (arg is Staff)
                        {
                            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete staff member {(arg as Staff).GetIdentifier()}?",
                                "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                            if (result == MessageBoxResult.Yes)
                            {
                                StaffDatabaseHelper.DeleteStaff((Staff)arg);
                                _staffMembers = new ObservableCollection<Staff>(StaffDatabaseHelper.GetAllStaff());
                                OnPropertyChanged(nameof(StaffMembers));

                                UserActionLogEntry logEntry = new UserActionLogEntry(_loggedInUser.ToString(), _loggedInUser.Username,
                                    $"Deleted staff member {(arg as Staff).ToString()}");
                                Logger.WriteUserActionLogEntry(logEntry);
                            }
                        }
                    },
                    arg => true);
                }

                return _deleteStaffMemberCommand;
            }
        }

        #endregion
    }
}
