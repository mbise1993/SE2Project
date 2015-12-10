using HopeHouse.Common.EventFramework;
using System;
using System.Reflection;
using System.Windows;
using HopeHouse.Common.Interfaces;
using HopeHouse.Core.DataAccess;
using HopeHouse.Exe.Controls;
using HopeHouse.Exe.Windows;
using HopeHouse.Core.Models;

namespace HopeHouse.Exe.EventFramework
{
    internal static class ExeEventHandlers
    {
        #region Public Methods

        public static void Subscribe()
        {
            EventProvider.GetWindow += OnGetWindow;
            EventProvider.ShowWindow += OnShowWindow;
            EventProvider.ShowDialog += OnShowDialog;
            EventProvider.AddDataProviderDisplay += OnAddDataProviderDisplay;
            EventProvider.AddDataProviderAggregationDisplay += OnAddDataProviderAggregationDisplay;
            EventProvider.AddNewClientDisplay += OnAddNewClientDisplay;
            EventProvider.ViewStaffMembersDisplay += OnViewStaffMembersDisplay;
            EventProvider.ViewUserActionLogDisplay += OnViewUserActionLogDisplay;
            EventProvider.AddAppliedFilterEvent += OnAddAppliedFilter;
        }

        #endregion

        #region Event Handlers

        private static Window OnGetWindow(string windowType)
        {
            if(!string.IsNullOrEmpty(windowType))
            {
                foreach(Window window in Application.Current.Windows)
                {
                    if(window.GetType().Name == windowType)
                    {
                        return window;
                    }
                }
            }

            return null;
        }

        private static void OnShowWindow(string windowType, object[] parameters)
        {
            if (!string.IsNullOrEmpty(windowType))
            {
                Type windowTypeToShow = Assembly.GetExecutingAssembly().GetType("HopeHouse.Exe.Windows." + windowType);

                if (windowTypeToShow != null)
                {
                    Window windowToShow = Activator.CreateInstance(windowTypeToShow, parameters) as Window;

                    if(windowToShow != null)
                    {
                        windowToShow.Show();
                    }
                }
            }
        }

        private static bool? OnShowDialog(string dialogType, object[] parameters)
        {
            if (!string.IsNullOrEmpty(dialogType))
            {
                Type dialogTypeToShow = Assembly.GetExecutingAssembly().GetType("HopeHouse.Exe.Windows." + dialogType);

                if (dialogTypeToShow != null)
                {
                    Window dialogToShow = Activator.CreateInstance(dialogTypeToShow, parameters) as Window;

                    if (dialogToShow != null)
                    {
                        return dialogToShow.ShowDialog();
                    }
                }
            }

            return null;
        }

        private static void OnAddDataProviderDisplay(IDataProvider dataProvider)
        {
            if (dataProvider != null)
            {
                MainWindow mainWindow = null;

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        mainWindow = window as MainWindow;
                    }
                }

                if (mainWindow != null)
                {
                    mainWindow.AddTab(new DataProviderInfoControl(dataProvider), dataProvider.GetIdentifier());
                }
            }
        }

        private static void OnAddDataProviderAggregationDisplay(int ownerId, IDataProviderAggregation dataProviderAggregation)
        {
            if (dataProviderAggregation != null)
            {
                MainWindow mainWindow = null;

                foreach (Window window in Application.Current.Windows)
                {
                    if (window is MainWindow)
                    {
                        mainWindow = window as MainWindow;
                    }
                }

                if (mainWindow != null)
                {
                    mainWindow.AddTab(new DataProviderAggregationInfoControl(ownerId, dataProviderAggregation), 
                        dataProviderAggregation.GetIdentifier());
                }
            }
        }

        private static void OnAddNewClientDisplay(object loggedInUser)
        {
            MainWindow mainWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    mainWindow = window as MainWindow;
                }

                if (mainWindow != null)
                {
                    mainWindow.AddTab(new NewClientControl((Staff)loggedInUser), "New Client");
                }
            }
        }

        private static void OnViewStaffMembersDisplay(object loggedInUser)
        {
            MainWindow mainWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    mainWindow = window as MainWindow;
                }

                if (mainWindow != null)
                {
                    mainWindow.AddTab(new ViewStaffMembersControl((Staff)loggedInUser), "Staff Members");
                }
            }
        }

        private static void OnViewUserActionLogDisplay()
        {
            MainWindow mainWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    mainWindow = window as MainWindow;
                }

                if (mainWindow != null)
                {
                    mainWindow.AddTab(new LogViewerControl(), "User Action Log");
                }
            }
        }

        private static void OnAddAppliedFilter(object filter)
        {
            if(!(filter is Filter))
            {
                return;
            }

            MainWindow mainWindow = null;

            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow)
                {
                    mainWindow = window as MainWindow;
                }

                if (mainWindow != null)
                {
                    mainWindow.AddAppliedFilter((Filter)filter);
                }
            }
        }

        #endregion
    }
}
