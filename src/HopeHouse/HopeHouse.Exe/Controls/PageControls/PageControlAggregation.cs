using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeHouse.Exe.Controls.PageControls;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Exe.Controls
{
    public class PageControlAggregation : ViewModelBase
    {
        #region Private Fields

        private NewClientViewModel _viewModel;
        private LinkedList<PageControlWrapper> _pages;
        private LinkedListNode<PageControlWrapper> _currentPageNode;

        #endregion

        #region Properties

        public PageControlWrapper CurrentPage
        {
            get
            {
                if (_currentPageNode == null)
                {
                    _currentPageNode = _pages.First;
                }

                return _currentPageNode.Value;
            }
            set
            {
                _currentPageNode.Value = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        #endregion

        #region Constructor

        public PageControlAggregation(NewClientViewModel viewModel)
        {
            _viewModel = viewModel;
            _pages = new LinkedList<PageControlWrapper>();

            PopulatePages();
        }

        #endregion

        #region Public Methods

        public void NavigateBack()
        {
            _currentPageNode = _currentPageNode.Previous;
            OnPropertyChanged(nameof(CurrentPage));
        }

        public void NavigateForward()
        {
            _currentPageNode = _currentPageNode.Next;
            OnPropertyChanged(nameof(CurrentPage));
        }

        #endregion

        #region Private Methods

        private void PopulatePages()
        {
            PageControlWrapper page1 = new PageControlWrapper(true, false, new NewClientPage1(_viewModel));
            PageControlWrapper page2 = new PageControlWrapper(false, false, new NewClientPage2(_viewModel));
            PageControlWrapper page3 = new PageControlWrapper(false, false, new NewClientPage3(_viewModel));
            PageControlWrapper page4 = new PageControlWrapper(false, true, new NewClientPage4(_viewModel));

            _pages.AddLast(new LinkedListNode<PageControlWrapper>(page1));
            _pages.AddLast(new LinkedListNode<PageControlWrapper>(page2));
            _pages.AddLast(new LinkedListNode<PageControlWrapper>(page3));
            _pages.AddLast(new LinkedListNode<PageControlWrapper>(page4));
        }

        #endregion
    }
}
