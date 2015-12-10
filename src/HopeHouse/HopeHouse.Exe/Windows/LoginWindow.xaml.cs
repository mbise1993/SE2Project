using HopeHouse.Presentation.ViewModels;
using System.Windows;

namespace HopeHouse.Exe.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        #region Event Handlers

        /// <summary>
        /// When passwordBoxFacade gets focus, hide it, display actual passwordBox, and give it focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordBoxFacade_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBoxFacade.Visibility = Visibility.Collapsed;
            PasswordBox.Focus();
        }

        /// <summary>
        /// If passwordBox is empty when it loses focus, hide it and show passwordBoxFacade
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(PasswordBox.Password.Length == 0)
            {
                PasswordBoxFacade.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// When entered password changes, set password property in view model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            LoginWindowViewModel viewModel = this.DataContext as LoginWindowViewModel;

            if(viewModel != null)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }

        #endregion
    }
}
