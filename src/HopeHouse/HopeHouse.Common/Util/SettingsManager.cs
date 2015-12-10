using System.Windows;
using HopeHouse.Common.Properties;

namespace HopeHouse.Common.Util
{
    public static class SettingsManager
    {
        #region Private Default Fields

        private static readonly string _defaultDatabasePath = "../Database/HopeHouse.sqlite";
        private static readonly string _defaultLastBackupDate = "Never";
        private static readonly string _defaultBackupReminder = "NextRun";

        #endregion

        #region Public Methods

        public static void RestoreSettingsToDefaults()
        {
            Settings.Default.Reset();
        }

        public static void SaveSettings()
        {
            Settings.Default.Save();
        }

        public static void SetLastBackupDate(string lastBackupDate = null)
        {
            string value = string.IsNullOrEmpty(lastBackupDate) ? _defaultLastBackupDate : lastBackupDate;
            Settings.Default.LastBackupDate = value;
        }

        public static void SetBackupReminder(string backupReminder = null)
        {
            string value = string.IsNullOrEmpty(backupReminder) ? _defaultBackupReminder : backupReminder;
            Settings.Default.BackupReminder = value;
        }

        public static void SetAutoBackupPath(string autoBackupPath)
        {
            Settings.Default.AutoBackupPath = autoBackupPath;
        }

        public static string GetDatabasePath()
        {
            if (string.IsNullOrEmpty(Settings.Default.DatabasePath))
            {
                Settings.Default.DatabasePath = _defaultDatabasePath;
            }

            return Settings.Default.DatabasePath;
        }

        public static string GetLastBackupDate()
        {
            if (string.IsNullOrEmpty(Settings.Default.LastBackupDate))
            {
                SetLastBackupDate();
            }

            return Settings.Default.LastBackupDate;
        }

        public static string GetBackupReminder()
        {
            if (string.IsNullOrEmpty(Settings.Default.BackupReminder))
            {
                SetBackupReminder();
            }

            return Settings.Default.BackupReminder;
        }

        public static string GetAutoBackupPath()
        {
            return Settings.Default.AutoBackupPath;
        }

        #endregion
    }
}
