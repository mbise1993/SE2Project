using System.Windows;
using HopeHouse.Common.Interfaces;

namespace HopeHouse.Common.EventFramework
{
    public static class EventProvider
    {
        #region Get Window Event

        public delegate Window GetWindowHandler(string windowType);
        public static event GetWindowHandler GetWindow;
        public static Window PublishGetWindow(string windowType)
        {
            if(GetWindow != null)
            {
                return GetWindow(windowType);
            }

            return null;
        }

        #endregion

        #region Show Window Event

        public delegate void ShowWindowHandler(string windowType, object[] parameters);
        public static event ShowWindowHandler ShowWindow;
        public static void PublishShowWindow(string windowType, object[] parameters = null)
        {
            if(ShowWindow != null)
            {
                ShowWindow(windowType, parameters);
            }
        }

        #endregion

        #region Show Dialog Event

        public delegate bool? ShowDialogHandler(string windowType, object[] parameters);
        public static event ShowDialogHandler ShowDialog;
        public static bool? PublishShowDialog(string windowType, object[] parameters = null)
        {
            return ShowWindow != null ? ShowDialog?.Invoke(windowType, parameters) : null;
        }

        #endregion

        #region Add Data Provider Display Event

        public delegate void AddDataProviderDisplayHandler(IDataProvider dataProvider);
        public static event AddDataProviderDisplayHandler AddDataProviderDisplay;
        public static void PublishAddDataProviderDisplay(IDataProvider dataProvider)
        {
            if (AddDataProviderDisplay != null)
            {
                AddDataProviderDisplay(dataProvider);
            }
        }

        #endregion

        #region Add Data Provider Aggregation Display Event

        public delegate void AddDataProviderAggregationDisplayEvent(int clientId, IDataProviderAggregation dataProviderAggregation);
        public static event AddDataProviderAggregationDisplayEvent AddDataProviderAggregationDisplay;
        public static void PublishAddDataProviderAggregationDisplay(int clientId, IDataProviderAggregation dataProviderAggregation)
        {
            if(AddDataProviderAggregationDisplay != null)
            {
                AddDataProviderAggregationDisplay(clientId, dataProviderAggregation);
            }
        }

        #endregion

        #region Add New Client Display Event

        public delegate void AddNewClientDisplayHandler(object loggedInUser);
        public static event AddNewClientDisplayHandler AddNewClientDisplay;
        public static void PublishAddNewClientDisplay(object loggedInUser)
        {
            if (AddNewClientDisplay != null)
            {
                AddNewClientDisplay(loggedInUser);
            }
        }

        #endregion

        #region View Staff Members Display Event

        public delegate void ViewStaffMembersDisplayHandler(object loggedInUser);
        public static event ViewStaffMembersDisplayHandler ViewStaffMembersDisplay;
        public static void PublishViewStaffMembersDisplay(object loggedInUser)
        {
            if (ViewStaffMembersDisplay != null)
            {
                ViewStaffMembersDisplay(loggedInUser);
            }
        }

        #endregion

        #region View User Action Log Display Event

        public delegate void ViewUserActionLogDisplayHandler();
        public static event ViewUserActionLogDisplayHandler ViewUserActionLogDisplay;
        public static void PublishViewUserActionLogDisplay()
        {
            if (ViewUserActionLogDisplay != null)
            {
                ViewUserActionLogDisplay();
            }
        }

        #endregion

        #region Add Applied Filter Event

        public delegate void AddAppliedFilterEventHandler(object filter);
        public static event AddAppliedFilterEventHandler AddAppliedFilterEvent;
        public static void PublishAddAppliedFilter(object filter)
        {
            if (AddAppliedFilterEvent != null)
            {
                AddAppliedFilterEvent(filter);
            }
        }

        #endregion
    }
}
