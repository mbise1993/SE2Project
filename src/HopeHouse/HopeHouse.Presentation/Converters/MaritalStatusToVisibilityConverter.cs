using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class MaritalStatusToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var comboBoxItem = value as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    string selectedItem = comboBoxItem.Content.ToString();

                    if (selectedItem == "Married" || selectedItem == "In Relationship")
                    {
                        return Visibility.Visible;
                    }
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Please add correct exceptions instead of NotImplemented.
            throw new NotImplementedException();
        }
    }
}
