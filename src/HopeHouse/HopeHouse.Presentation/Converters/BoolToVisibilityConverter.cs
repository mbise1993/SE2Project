using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool) && (parameter is string))
            {
                if ((string)parameter == "VisibleOnTrue")
                {
                    return (bool)value ? Visibility.Visible : Visibility.Collapsed;
                }

                if ((string) parameter == "VisibleOnTrueHidden")
                {
                    return (bool) value ? Visibility.Visible : Visibility.Hidden;
                }

                if((string)parameter == "VisibleOnFalse")
                {
                    return !(bool)value ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
