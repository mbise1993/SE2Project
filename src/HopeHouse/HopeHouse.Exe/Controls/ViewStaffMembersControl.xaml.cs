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
using HopeHouse.Presentation.ViewModels;
using HopeHouse.Core.Models;

namespace HopeHouse.Exe.Controls
{
    /// <summary>
    /// Interaction logic for ViewStaffMembersControl.xaml
    /// </summary>
    public partial class ViewStaffMembersControl : UserControl
    {
        #region Private Fields

        private ViewStaffMembersViewModel _viewModel;

        #endregion

        #region Properties

        public ViewStaffMembersViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
        }

        #endregion

        public ViewStaffMembersControl(Staff loggedInUser)
        {
            InitializeComponent();

            (Content as FrameworkElement).DataContext = this;
            _viewModel = new ViewStaffMembersViewModel(loggedInUser);
        }
    }
}
