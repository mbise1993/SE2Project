using System;
using System.Globalization;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class DatabaseConnectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(targetType == typeof(string))
            {
                return (bool)value ? "Database connected" : "Database disconnected";
            }

            if(targetType == typeof(Uri))
            {

            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
