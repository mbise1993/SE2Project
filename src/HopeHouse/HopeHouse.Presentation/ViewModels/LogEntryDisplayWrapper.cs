using HopeHouse.Common.Logging;

namespace HopeHouse.Presentation.ViewModels
{
    public class LogEntryDisplayWrapper : ViewModelBase
    {
        #region Private Fields

        private UserActionLogEntry _logEntry;
        private bool _isDisplayed;

        #endregion

        #region Properties

        public UserActionLogEntry LogEntry
        {
            get
            {
                return _logEntry;
            }
        }

        public bool IsDisplayed
        {
            get
            {
                return _isDisplayed;
            }
            set
            {
                _isDisplayed = value;
                OnPropertyChanged(nameof(IsDisplayed));
            }
        }

        #endregion

        #region Constructor

        public LogEntryDisplayWrapper(UserActionLogEntry logEntry)
        {
            _logEntry = logEntry;
        }

        #endregion
    }
}
