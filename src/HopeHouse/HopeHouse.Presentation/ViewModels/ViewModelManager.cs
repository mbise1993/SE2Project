namespace HopeHouse.Presentation.ViewModels
{
    public class ViewModelManager
    {
        #region Private Fields

        private MainWindowViewModel _mainWindowVm;

        #endregion

        #region Properties

        public MainWindowViewModel MainWindowVm
        {
            get
            {
                return _mainWindowVm;
            }
        }

        #endregion

        #region Constructor

        public ViewModelManager(MainWindowViewModel mainWindowVm)
        {
            _mainWindowVm = mainWindowVm;
        }

        #endregion
    }
}
