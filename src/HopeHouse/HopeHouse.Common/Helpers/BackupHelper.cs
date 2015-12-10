using System;
using System.IO;
using System.Windows.Forms;
using HopeHouse.Common.Util;

namespace HopeHouse.Common.Helpers
{
    public static class BackupHelper
    {
        #region Public Methods

        public static void UseBackupDatabase(string backupFilePath)
        {
            if (File.Exists(backupFilePath))
            {
                string defaultPath = SettingsManager.GetDatabasePath();

                if (File.Exists(defaultPath))
                {
                    string defaultDirectory = Path.GetDirectoryName(defaultPath);

                    int i = 2;
                    string newFileName = Path.Combine(defaultDirectory, $"HopeHouse{i}.sqlite");

                    while (File.Exists(newFileName))
                    {
                        newFileName = Path.Combine(defaultDirectory, $"HopeHouse{++i}.sqlite");
                    }

                    File.Move(defaultPath, newFileName);
                }

                File.Copy(backupFilePath, defaultPath);
            }
        }

        public static bool PerformAutomaticDatabaseBackup()
        {
            string backupPath = SettingsManager.GetAutoBackupPath();

            if (Directory.Exists(backupPath))
            {
                BackupDatabase(backupPath);
                return true;
            }

            return false;
        }

        public static void BackupDatabase(string backupDirectory)
        {
            if (Directory.Exists(backupDirectory))
            {
                string fileName = Path.GetFileName(SettingsManager.GetDatabasePath());
                string backupPath = Path.Combine(backupDirectory, fileName);

                if (File.Exists(backupPath))
                {
                    File.Delete(backupPath);
                }

                File.Copy(SettingsManager.GetDatabasePath(), backupPath);
                SettingsManager.SetLastBackupDate(DateTime.Now.ToShortDateString());
            }
        }

        #endregion
    }
}
