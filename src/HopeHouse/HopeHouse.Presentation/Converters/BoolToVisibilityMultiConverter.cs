using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class BoolToVisibilityMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if ((values[0] is bool) && (values[1] is bool) && (parameter is string))
            {
                if ((string) parameter == "VisibleOnTrueTrue")
                {
                    return ((bool) values[0] && (bool) values[1]) ? Visibility.Visible : Visibility.Collapsed;
                }

                if ((string)parameter == "VisibleOnTrueFalse")
                {
                    return ((bool)values[0] && !(bool)values[1]) ? Visibility.Visible : Visibility.Collapsed;
                }

                if ((string)parameter == "VisibleOnFalseTrue")
                {
                    return (!(bool)values[0] && (bool)values[1]) ? Visibility.Visible : Visibility.Collapsed;
                }

                if ((string)parameter == "VisibleOnFalseFalse")
                {
                    return (!(bool)values[0] && !(bool)values[1]) ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
