using HopeHouse.Core.Models;
using HopeHouse.Exe.Controls;
using HopeHouse.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HopeHouse.Common.Helpers;
using HopeHouse.Common.Util;
using HopeHouse.Core.DataAccess;

namespace HopeHouse.Exe.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields

        private MainWindowViewModel _viewModel;

        #endregion

        #region Constructor

        public MainWindow(Staff loggedInUser)
        {
            InitializeComponent();

            _viewModel = this.DataContext as MainWindowViewModel;

            if(_viewModel != null)
            {
                _viewModel.LoggedInUser = loggedInUser;
            }
        }

        #endregion

        #region Event Handlers

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string backupReminder = SettingsManager.GetBackupReminder();

            if (backupReminder == "AutomaticBackupsOn")
            {
                if (!BackupHelper.PerformAutomaticDatabaseBackup())
                {
                    MessageBox.Show("Unable to access location set as automatic backup path. Go to 'Database'->'Change Backup Options' " +
                        "in the menu to change your backup settings.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (backupReminder == "NextRun" || backupReminder == DateTime.Now.ToShortDateString())
            {
                DatabaseBackupSettingsWindow backupSettingsWindow = new DatabaseBackupSettingsWindow();
                backupSettingsWindow.ShowDialog();
            }
    }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(_viewModel != null)
            {
                AddTab(new DataProviderInfoControl(_viewModel.SelectedClient), _viewModel.SelectedClient.ToString());
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown(Environment.ExitCode);
        }

        private void TabItemCloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).CommandParameter is TabItem)
            {
                InfoTabControl.Items.Remove((TabItem)(sender as Button).CommandParameter);

                int indexToSelect = InfoTabControl.Items.Count - 1;
                InfoTabControl.SelectedItem = InfoTabControl.Items[indexToSelect];
            }
        }

        private void PresetFilter_Click(object sender, RoutedEventArgs e)
        {
            DefaultFiltersButton.IsOpen = false;
        }

        #endregion

        #region Public Methods

        public void AddTab(Control content, string headerText)
        {
            TabItem dataProviderTab = new TabItem();
            dataProviderTab.Header = headerText;
            dataProviderTab.HeaderTemplate = FindResource("CloseTabItemHeaderTemplate") as DataTemplate;
            content.Padding = new Thickness(4);
            dataProviderTab.Content = content;

            InfoTabControl.Items.Add(dataProviderTab);
            dataProviderTab.Focus();
        }

        public void AddAppliedFilter(Filter filter)
        {
            Button filterButton = new Button();
            filterButton.Tag = filter;
            filterButton.Style = FindResource("FilterButtonStyle") as Style;
        }

        #endregion
    }
}
