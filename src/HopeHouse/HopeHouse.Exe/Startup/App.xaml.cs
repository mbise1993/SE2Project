using HopeHouse.Core.DataAccess;
using HopeHouse.Exe.EventFramework;
using HopeHouse.Exe.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows;
using HopeHouse.Common.Logging;
using HopeHouse.Common.Util;
using HopeHouse.Core.Models;

namespace HopeHouse.Exe
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Events

        //
        // Initialize everything, start loading clients, and start the application
        //
        private void App_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            //
            // UNCOMMENT THIS LINE, RUN APP, THEN RECOMMENT IT OUT TO RESET SETTINGS
            //
            //SettingsManager.RestoreSettingsToDefaults();

            Logger.Initialize();
            ExeEventHandlers.Subscribe();

            InitializeDatabase();
            LoadClients();
            StartApplication();
        }

        //
        // On exit, stop all threads, close the DB connection, and save the settings
        //
        private void App_Exit(object sender, ExitEventArgs e)
        {
            WorkerThreadManager.StopAllThreads();
            Database.Close();
            SettingsManager.SaveSettings();
        }

        //
        // If an unhandled exception occurs, write a log entry and show error message box
        //
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.WriteErrorLogEntry(new ErrorLogEntry((Exception)e.ExceptionObject));

            if(e.IsTerminating)
            {
                MessageBox.Show("The application experienced a problem and has to close. The error log is located at\n" +
                    $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\HopeHouse\\Errors.log", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Private Methods

        private void InitializeDatabase()
        {
            string databasePath = SettingsManager.GetDatabasePath();

            //
            // TODO: If database is installed with app, we don't need to check for it on
            //       run. We can just let the error handling mechanism handle any problems
            //       that may occur.
            //
            //if (!File.Exists(databasePath))
            //{
            //    Database.CreateDatabase(SettingsManager.GetDatabasePath());
            //}

            bool initSuccessful = false;
            int attemptsLeft = 3;

            while (attemptsLeft > 0)
            {
                if (Database.Initialize(databasePath))
                {
                    initSuccessful = true;
                    break;
                }

                DatabaseInitializationErrorWindow errorWindow = new DatabaseInitializationErrorWindow();
                errorWindow.ShowDialog();

                attemptsLeft--;
            }

            if (!initSuccessful)
            {
                MessageBox.Show("Failed to initialize database in 3 attempts. Exiting.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);

                Shutdown(Environment.ExitCode);
            }
        }

        private void LoadClients()
        {
            Stopwatch timer = new Stopwatch();

            BackgroundWorker loadClientsWorker = new BackgroundWorker();
            loadClientsWorker.WorkerSupportsCancellation = true;

            loadClientsWorker.DoWork += (sender, args) =>
            {
                timer.Start();
                ClientManager.LoadClients();
            };

            loadClientsWorker.RunWorkerCompleted += (sender, args) =>
            {
                timer.Stop();
                Trace.WriteLine($"Time to load {ClientManager.GetNumberOfClients()} Clients: {timer.Elapsed.TotalSeconds}");
            };

            WorkerThreadManager.AddWorkerThread(loadClientsWorker);
            loadClientsWorker.RunWorkerAsync();
        }

        private void StartApplication()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowInTaskbar = false;

            if (loginWindow.ShowDialog() != true)
            {
                this.Shutdown(Environment.ExitCode);
            }
        }

        #endregion
    }
}
