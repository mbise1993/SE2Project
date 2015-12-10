using System.Windows;
using System.Windows.Controls;
using HopeHouse.Presentation.ViewModels;
using HopeHouse.Common.Interfaces;

namespace HopeHouse.Exe.Controls
{
    public enum ClientInfoSearchBy
    {
        Property,
        Value
    }

    /// <summary>
    /// Interaction logic for DataProviderInfoControl.xaml
    /// </summary>
    public partial class DataProviderInfoControl : UserControl
    {
        #region Private Fields

        private DataProviderViewModel _viewModel;
        private IDataProvider _dataProvider;

        #endregion

        #region Properties

       public DataProviderViewModel ViewModel
        {
            set
            {
                _viewModel = value;
                SetValue(ViewModelProperty, value);
            }
        }

        public IDataProvider DataProvider
        {
            set
            {
                _dataProvider = value;
                SetValue(DataProviderProperty, value);
            }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(DataProviderViewModel),
            typeof(DataProviderInfoControl));

        public static readonly DependencyProperty DataProviderProperty = DependencyProperty.Register(
            "DataProvider",
            typeof(IDataProvider),
            typeof(DataProviderInfoControl));

        #endregion

        #region Constructor

        public DataProviderInfoControl(IDataProvider dataProvider)
        {
            InitializeComponent();

            _dataProvider = dataProvider;

            (Content as FrameworkElement).DataContext = this;
            ViewModel = new DataProviderViewModel(_dataProvider);
        }

        #endregion
    }
}
