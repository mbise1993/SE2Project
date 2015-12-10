using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HopeHouse.Presentation.ViewModels;
using HopeHouse.Core.Models;

namespace HopeHouse.Exe.Controls
{
    /// <summary>
    /// Interaction logic for NewClientControl.xaml
    /// </summary>
    public partial class NewClientControl : UserControl
    {
        #region Private Fields

        private NewClientViewModel _viewModel;
        private PageControlAggregation _pages;

        #endregion

        #region Properties

        public NewClientViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        public PageControlAggregation Pages
        {
            get
            {
                return _pages;
            }
        }

        #endregion

        #region Constructor

        public NewClientControl(Staff loggedInUser)
        {
            InitializeComponent();

            (Content as FrameworkElement).DataContext = this;
            _viewModel = new NewClientViewModel(loggedInUser);
            _pages = new PageControlAggregation(_viewModel);
        }

        #endregion

        #region Event Handlers

        public void NavBackButton_Click(object sender, RoutedEventArgs e)
        {
            _pages.NavigateBack();
        }

        public void NavForwardButton_Click(object sender, RoutedEventArgs e)
        {
            _pages.NavigateForward();
        }

        #endregion
    }
}
