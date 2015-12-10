using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool) value)
                {
                    return "Yes";
                }

                return "No";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string valueAsString;

                if (value is ComboBoxItem)
                {
                    valueAsString = (value as ComboBoxItem).Content.ToString();
                }
                else
                {
                    valueAsString = value.ToString();
                }

                if (valueAsString == "Yes")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
