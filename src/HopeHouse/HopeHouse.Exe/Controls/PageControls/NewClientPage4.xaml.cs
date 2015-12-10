using System.Windows;
using System.Windows.Controls;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Exe.Controls.PageControls
{
    /// <summary>
    /// Interaction logic for NewClientPage1.xaml
    /// </summary>
    public partial class NewClientPage4 : UserControl
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

        public NewClientPage4(NewClientViewModel viewModel)
        {
            InitializeComponent();

            (Content as FrameworkElement).DataContext = this;
            _viewModel = viewModel;
        }
    }
}
