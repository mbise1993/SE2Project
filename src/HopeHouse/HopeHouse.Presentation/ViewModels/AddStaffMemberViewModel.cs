using System.Windows;
using System.Windows.Input;
using HopeHouse.Common.EventFramework;
using HopeHouse.Common.Helpers;
using HopeHouse.Core.DataAccess;
using HopeHouse.Core.Models;
using HopeHouse.Presentation.Commanding;

namespace HopeHouse.Presentation.ViewModels
{
    public class AddStaffMemberViewModel : ViewModelBase
    {
        #region Private Fields

        private string _firstName;
        private string _lastName;
        private string _middleInit;
        private string _userName;
        private string _password;
        private string _phoneNumber;
        private bool _isAdministrator;

        #endregion

        #region Properties

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        public string MiddleInit
        {
            get
            {
                return _middleInit;
            }
            set
            {
                _middleInit = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
            }
        }

        public bool IsAdministrator
        {
            get
            {
                return _isAdministrator;
            }
            set
            {
                _isAdministrator = value;
            }
        }

        #endregion

        #region Commands

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(arg =>
                    {
                        Window dialog = EventProvider.PublishGetWindow("AddStaffMemberWindow");

                        if (dialog != null)
                        {
                            dialog.DialogResult = false;
                        }
                    },
                    arg => true);
                }

                return _cancelCommand;
            }
        }

        private ICommand _addStaffMemberCommand;
        public ICommand AddStaffMemberCommand
        {
            get
            {
                if (_addStaffMemberCommand == null)
                {
                    _addStaffMemberCommand = new RelayCommand(arg =>
                    {
                        Staff staffMemberToAdd = new Staff
                        {
                            FirstName = _firstName,
                            LastName = _lastName,
                            MiddleInit = _middleInit,
                            Username = _userName,
                            Password = PasswordEncrypt.EncryptPassword(_password),
                            PhoneNumber = _phoneNumber,
                            IsAdministrator = _isAdministrator
                        };

                        StaffDatabaseHelper.AddStaff(staffMemberToAdd);

                        Window dialog = EventProvider.PublishGetWindow("AddStaffMemberWindow");

                        if (dialog != null)
                        {
                            dialog.DialogResult = true;
                        }
                    },
                    arg => (!string.IsNullOrEmpty(_firstName)) && (!string.IsNullOrEmpty(_lastName)) 
                        && (!string.IsNullOrEmpty(_middleInit)) && (!string.IsNullOrEmpty(_userName)) 
                        && (!string.IsNullOrEmpty(_password)) && (!string.IsNullOrEmpty(_phoneNumber)));
                }

                return _addStaffMemberCommand;
            }
        }

        #endregion
    }
}
