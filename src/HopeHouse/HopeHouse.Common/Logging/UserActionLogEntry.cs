using System;

namespace HopeHouse.Common.Logging
{
    public class UserActionLogEntry
    {
        #region Private Fields

        private string _userFullName;
        private string _userUserName;
        private string _userAction;
        private DateTime _date;

        #endregion

        #region Properties

        public string UserFullName
        {
            get
            {
                return _userFullName;
            }
        }

        public string UserUserName
        {
            get
            {
                return _userUserName;
            }
        }

        public string UserAction
        {
            get
            {
                return _userAction;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        #endregion

        #region Constructor

        public UserActionLogEntry(string userFullName, string userUserName, string userAction) : 
            this(userFullName, userUserName, userAction, DateTime.Now)
        {
            
        }

        public UserActionLogEntry(string userFullName, string userUserName, string userAction, DateTime date)
        {
            _userFullName = userFullName;
            _userUserName = userUserName;
            _userAction = userAction;
            _date = date;
        }

        #endregion
    }
}
