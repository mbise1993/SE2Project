using System.Windows.Controls;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Exe.Controls
{
    public class PageControlWrapper
    {
        #region Private Fields

        private bool _isFirstPage;
        private bool _isLastPage;
        private Control _page;

        #endregion

        #region Properties

        public bool IsFirstPage
        {
            get
            {
                return _isFirstPage;
            }
        }

        public bool IsLastPage
        {
            get
            {
                return _isLastPage;
            }
        }

        public Control Page
        {
            get
            {
                return _page;
            }
        }

        #endregion

        #region Constructor

        public PageControlWrapper(bool isFirstPage, bool isLastPage, Control page)
        {
            _isFirstPage = isFirstPage;
            _isLastPage = isLastPage;
            _page = page;
        }

        #endregion
    }
}
