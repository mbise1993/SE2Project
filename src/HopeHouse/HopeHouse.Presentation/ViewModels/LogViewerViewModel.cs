using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using HopeHouse.Common.Logging;

namespace HopeHouse.Presentation.ViewModels
{
    public enum LogEntriesSearchBy
    {
        ShowDefault,
        FullName,
        Username,
        Date,
        Action
    }

    public class LogViewerViewModel : ViewModelBase
    {
        #region Private Fields

        private readonly LogEntriesSearchBy[] _searchOptions =
        {
            LogEntriesSearchBy.FullName,
            LogEntriesSearchBy.Username,
            LogEntriesSearchBy.Date,
            LogEntriesSearchBy.Action
        };
        private LogEntriesSearchBy _searchBy = LogEntriesSearchBy.ShowDefault;
        private string _searchString;
        private ICollection<LogEntryDisplayWrapper> _logEntries;

        #endregion

        #region Properties

        public IEnumerable<LogEntriesSearchBy> SearchOptions
        {
            get
            {
                return _searchOptions;
            }
        }

        public LogEntriesSearchBy SearchBy
        {
            get
            {
                return _searchBy;
            }
            set
            {
                _searchBy = value;
                OnPropertyChanged(nameof(SearchBy));
                OnPropertyChanged(nameof(IsSearchStringTextBoxEnabled));
            }
        }

        public bool IsSearchStringTextBoxEnabled
        {
            get
            {
                return _searchBy != LogEntriesSearchBy.ShowDefault;
            }
        }

        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
                UpdateLogEntries();
            }
        }

        public ICollection<LogEntryDisplayWrapper> LogEntries
        {
            get
            {
                if (_logEntries == null)
                {
                    _logEntries = new ObservableCollection<LogEntryDisplayWrapper>();
                }

                return _logEntries;
            }
            set
            {
                _logEntries = value;
                OnPropertyChanged(nameof(LogEntries));
            }
        }

        #endregion

        #region Constructor

        public LogViewerViewModel()
        {
            _logEntries = new ObservableCollection<LogEntryDisplayWrapper>();

            foreach (UserActionLogEntry logEntry in Logger.ReadUserActionLogEntries())
            {
                _logEntries.Add(new LogEntryDisplayWrapper(logEntry) { IsDisplayed = true });
            }
        }

        #endregion

        #region Private Methods

        private void UpdateLogEntries()
        {
            if (_searchString != null)
            {
                if (_searchBy == LogEntriesSearchBy.FullName)
                {
                    foreach (LogEntryDisplayWrapper logEntryWrapper in _logEntries)
                    {
                        if (logEntryWrapper.LogEntry.UserFullName.IndexOf(_searchString, StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            logEntryWrapper.IsDisplayed = true;
                        }
                        else
                        {
                            logEntryWrapper.IsDisplayed = false;
                        }
                    }
                }
                else if (_searchBy == LogEntriesSearchBy.Username)
                {
                    foreach (LogEntryDisplayWrapper logEntryWrapper in _logEntries)
                    {
                        if (logEntryWrapper.LogEntry.UserUserName.IndexOf(_searchString, StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            logEntryWrapper.IsDisplayed = true;
                        }
                        else
                        {
                            logEntryWrapper.IsDisplayed = false;
                        }
                    }
                }
                else if (_searchBy == LogEntriesSearchBy.Action)
                {
                    foreach (LogEntryDisplayWrapper logEntryWrapper in _logEntries)
                    {
                        if (logEntryWrapper.LogEntry.UserAction.IndexOf(_searchString, StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            logEntryWrapper.IsDisplayed = true;
                        }
                        else
                        {
                            logEntryWrapper.IsDisplayed = false;
                        }
                    }
                }
                else if (_searchBy == LogEntriesSearchBy.Date)
                {
                    foreach (LogEntryDisplayWrapper logEntryWrapper in _logEntries)
                    {
                        if (logEntryWrapper.LogEntry.Date.ToShortDateString().IndexOf(
                            _searchString, StringComparison.InvariantCultureIgnoreCase) > -1)
                        {
                            logEntryWrapper.IsDisplayed = true;
                        }
                        else
                        {
                            logEntryWrapper.IsDisplayed = false;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
