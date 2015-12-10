namespace HopeHouse.Presentation.ViewModels
{
    public class DataDisplayWrapper : ViewModelBase
    {
        #region Private Fields

        private string _displayName;
        private object _value;
        private bool _isDisplayed;

        #endregion

        #region Properties

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool IsDisplayed
        {
            get
            {
                return _isDisplayed;
            }
            set
            {
                _isDisplayed = value;
                OnPropertyChanged(nameof(IsDisplayed));
            }
        }

        #endregion

        #region Constructor

        public DataDisplayWrapper(string displayName, object value)
        {
            _displayName = displayName;
            _value = value;
        }

        #endregion
    }
}
