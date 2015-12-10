using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class BoolStringToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                if ((string) value == "True")
                {
                    return 0;
                }

                if ((string) value == "False")
                {
                    return 1;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                string valueAsString;

                if ((int) value == 0)
                {
                    return "True";
                }

                if ((int) value == 1)
                {
                    return "False";
                }
            }

            return string.Empty;
        }
    }
}
