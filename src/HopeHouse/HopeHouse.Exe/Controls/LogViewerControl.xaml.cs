using System.Windows;
using System.Windows.Controls;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Exe.Controls
{
    /// <summary>
    /// Interaction logic for LogViewerControl.xaml
    /// </summary>
    public partial class LogViewerControl : UserControl
    {
        #region Private Fields

        private LogViewerViewModel _viewModel;

        #endregion

        #region Properties

        public LogViewerViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        #endregion

        #region Constructor

        public LogViewerControl()
        {
            InitializeComponent();

            (Content as FrameworkElement).DataContext = this;
            _viewModel = new LogViewerViewModel();
        }

        #endregion
    }
}
