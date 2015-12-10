using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace HopeHouse.Common.Logging
{
    public static class Logger
    {
        #region Private Fields

        private const string AppDebugLogFileName = @"Errors.log";
        private const string UserActionsLogFileName = @"UserActions.log";
        private static string _appDebugLogFilePath;
        private static string _userActionLogFilePath;
        private static XElement _appDebugLogRootElement;
        private static XElement _userActionsLogRootElement;

        #endregion

        #region Public Methods

        //
        // If log file doesn't exist, create it and create root element. If it does, set _rootElement
        //
        public static void Initialize()
        {
            string hopeHouseAppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HopeHouse");
            _appDebugLogFilePath = Path.Combine(hopeHouseAppDataPath, AppDebugLogFileName);
            _userActionLogFilePath = Path.Combine(hopeHouseAppDataPath, UserActionsLogFileName);

            if(!File.Exists(_appDebugLogFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_appDebugLogFilePath));
                using (File.Create(_appDebugLogFilePath)) { }
            }

            if (!File.Exists(_userActionLogFilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_userActionLogFilePath));
                using (File.Create(_userActionLogFilePath)) { }
            }

            if(new FileInfo(_appDebugLogFilePath).Length == 0)
            {
                XmlTextWriter writer = new XmlTextWriter(_appDebugLogFilePath, null);
                writer.WriteStartElement("Log");
                writer.WriteEndElement();
                writer.Close();
            }

            if (new FileInfo(_userActionLogFilePath).Length == 0)
            {
                XmlTextWriter writer = new XmlTextWriter(_userActionLogFilePath, null);
                writer.WriteStartElement("Log");
                writer.WriteEndElement();
                writer.Close();
            }

            _appDebugLogRootElement = XElement.Load(_appDebugLogFilePath);
            _userActionsLogRootElement = XElement.Load(_userActionLogFilePath);
        }

        //
        // Write entry to app debug log consisting of error message, stack trace, and date
        //
        public static void WriteErrorLogEntry(ErrorLogEntry logEntry)
        {
            if((_appDebugLogRootElement != null) && (logEntry != null))
            {
                XElement entryElement = new XElement("Entry",
                    new XElement("Date", logEntry.Date.ToShortDateString()));
                WriteExceptionElement(entryElement, logEntry.Exception);

                _appDebugLogRootElement.Add(entryElement);

                _appDebugLogRootElement.Save(_appDebugLogFilePath);
            }
        }

        //
        // Add all the inner elements of an exception to the root in hierarchical fashion
        //
        private static void WriteExceptionElement(XElement parentElement, Exception exception)
        {
            XElement exceptionElement = new XElement("Exception",
                new XElement("Type", exception.GetType().Name),
                new XElement("Message", exception.Message),
                new XElement("Source", exception.Source),
                new XElement("Target", exception.TargetSite),
                new XElement("StackTrace", exception.StackTrace));

            parentElement.Add(exceptionElement);

            if(exception.InnerException != null)
            {
                WriteExceptionElement(exceptionElement, exception.InnerException);
            }
        }

        //
        // Write entry to user action log consisting of action performed, user who performed it, and date
        //
        public static void WriteUserActionLogEntry(UserActionLogEntry logEntry)
        {
            if ((_userActionsLogRootElement != null) && (logEntry != null))
            {
                _userActionsLogRootElement.Add(
                    new XElement("Entry",
                        new XElement("Date", logEntry.Date.ToShortDateString()),
                        new XElement("User",
                            new XElement("FullName", logEntry.UserFullName),
                            new XElement("UserName", logEntry.UserUserName)),
                        new XElement("Action", logEntry.UserAction)));

                _userActionsLogRootElement.Save(_userActionLogFilePath);
            }   
        }

        //
        // Gets all the entries from the user action log file in the form of a Collection of LogEntry objects
        //
        public static ICollection<UserActionLogEntry> ReadUserActionLogEntries()
        {
            ICollection<UserActionLogEntry> logEntries = new Collection<UserActionLogEntry>();

            if (_userActionsLogRootElement != null)
            {
                foreach (XElement logEntry in _userActionsLogRootElement.Elements("Entry"))
                {
                    string userFullName = null;
                    string userUserName = null;
                    string userAction = null;
                    DateTime date = new DateTime();

                    XElement dateElement = logEntry.Element("Date");
                    if (dateElement != null)
                    {
                        date = DateTime.Parse(dateElement.Value);
                    }

                    XElement userElement = logEntry.Element("User");
                    if (userElement != null)
                    {
                        XElement userFullNameElement = userElement.Element("FullName");
                        if (userFullNameElement != null)
                        {
                            userFullName = userFullNameElement.Value;
                        }

                        XElement userUserNameElement = userElement.Element("UserName");
                        if (userUserNameElement != null)
                        {
                            userUserName = userUserNameElement.Value;
                        }
                    }

                    XElement actionElement = logEntry.Element("Action");
                    if (actionElement != null)
                    {
                        userAction = actionElement.Value;
                    }

                    if ((userFullName != null) && (userUserName != null) && (userAction != null))
                    {
                        logEntries.Add(new UserActionLogEntry(userFullName, userUserName, userAction, date));
                    }
                }
            }

            return logEntries;
        }

        //
        // Deletes the log file and re-initializes reader/writer
        //
        public static void DeleteLogFile()
        {
            if (File.Exists(UserActionsLogFileName))
            {
                File.Delete(UserActionsLogFileName);
            }

            Initialize();
        }

        #endregion
    }
}
