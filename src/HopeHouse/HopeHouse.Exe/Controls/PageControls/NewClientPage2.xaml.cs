using System;
using System.Windows;
using System.Windows.Controls;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Exe.Controls.PageControls
{
    /// <summary>
    /// Interaction logic for NewClientPage1.xaml
    /// </summary>
    public partial class NewClientPage2 : UserControl
    {
        #region Private Fields

        private NewClientViewModel _viewModel;

        #endregion

        #region Properties

        public NewClientViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        #endregion

        public NewClientPage2(NewClientViewModel viewModel)
        {
            InitializeComponent();
            
            ChildBDayDateTimePicker.Maximum = DateTime.Today.AddDays ( 1 ).AddTicks ( -1 );

            (Content as FrameworkElement).DataContext = this;
            _viewModel = viewModel;
        }
    }
}
