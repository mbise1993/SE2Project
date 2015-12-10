using System;

namespace HopeHouse.Common.Logging
{
    public class ErrorLogEntry
    {
        #region Private Fields

        private Exception _exception;
        private DateTime _date;

        #endregion

        #region Properties

        public Exception Exception
        {
            get
            {
                return _exception;
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

        public ErrorLogEntry(Exception exception) : this(exception, DateTime.Now)
        {
            
        }

        public ErrorLogEntry(Exception exception, DateTime date)
        {
            _exception = exception;
            _date = date;
        }

        #endregion
    }
}
