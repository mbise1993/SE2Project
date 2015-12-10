using System;
using System.Windows;
using System.Windows.Input;
using HopeHouse.Presentation.Commanding;
using HopeHouse.Core.DataAccess;
using HopeHouse.Core.Models;
using HopeHouse.Common.Helpers;
using HopeHouse.Common.EventFramework;
using HopeHouse.Common.Logging;

namespace HopeHouse.Presentation.ViewModels
{
    public class LoginWindowViewModel : ViewModelBase
    {
        /****
        * Private Fields
        *****/
        private String _userName;
        private String _password;
        private String _errorMessage;

        /****
        * Properties
        *****/
        public String UserName
        {
            get
            {
                if (_userName == null)
                {
                    return String.Empty;
                }
                return _userName;
            }
            set
            {
                _userName = value.Trim();
                OnPropertyChanged ( nameof ( UserName ) );
            }
        }

        public String Password
        {
            set
            {
                _password = PasswordEncrypt.EncryptPassword ( value );
            }
        }

        public String ErrorMessage
        {
            get
            {
                if (_errorMessage == null)
                {
                    return String.Empty;
                }
                return _errorMessage;
            }
            private set
            {
                _errorMessage = value;
                OnPropertyChanged ( nameof ( ErrorMessage ) );
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError
        {
            get
            {
                return !String.IsNullOrEmpty(_errorMessage);
            }
        }

        /****
        * Commands
        *****/
        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(arg =>
                    {
                        Application.Current.Shutdown(Environment.ExitCode);
                    },
                        arg => true
                        );
                }

                return _cancelCommand;
            }
        }

        private ICommand _logInCommand;

        public ICommand LogInCommand
        {
            get
            {
                if (_logInCommand == null)
                {
                    _logInCommand = new RelayCommand(arg =>
                    {
                        // If-else here to find user, if user exists

                        Staff user = StaffDatabaseHelper.AuthenticateStaff ( _userName, _password );

                        if (user != null)
                        {
                            // Log action
                            Logger.WriteUserActionLogEntry(new UserActionLogEntry(user.ToString(), user.Username, "Logged in", DateTime.Now));

                            // If user is found, close login window
                            Window loginWindow = EventProvider.PublishGetWindow("LoginWindow");
                            if(loginWindow != null)
                            {
                                loginWindow.DialogResult = true;
                            }

                            //...and show main window
                            EventProvider.PublishShowWindow("MainWindow", new object[] { user });

                        }
                        else
                        {
                            ErrorMessage = "Incorrect username / password";
                        }
                    },
                    arg =>
                    {
                        if ((_userName != null) && (_password != null))
                            return true;

                        return false;
                    });
                }

                return _logInCommand;
            }
        }

    }
}
