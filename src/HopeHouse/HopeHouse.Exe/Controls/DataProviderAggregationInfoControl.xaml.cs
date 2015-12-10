using HopeHouse.Common.Interfaces;
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

namespace HopeHouse.Exe.Controls
{
    /// <summary>
    /// Interaction logic for DataProviderAggregationInfoControl.xaml
    /// </summary>
    public partial class DataProviderAggregationInfoControl : UserControl
    {
        #region Private Fields

        private DataProviderAggregationViewModel _viewModel;

        #endregion

        #region Properties

        public DataProviderAggregationViewModel ViewModel
        {
            set
            {
                _viewModel = value;
                SetValue(ViewModelProperty, value);
            }
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            "ViewModel",
            typeof(DataProviderAggregationViewModel),
            typeof(DataProviderAggregationInfoControl));

        #endregion

        public DataProviderAggregationInfoControl(int ownerId, IDataProviderAggregation dataProviderAggregation)
        {
            InitializeComponent();

            (Content as FrameworkElement).DataContext = this;
            ViewModel = new DataProviderAggregationViewModel(ownerId, dataProviderAggregation);
        }
    }
}
