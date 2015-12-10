using System.ComponentModel;

namespace HopeHouse.Presentation.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        //
        // INotifyPropertyChanged Implementations
        //
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
