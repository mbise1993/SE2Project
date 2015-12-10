using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class YesNoToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value != null) && (parameter != null))
            {
                var comboBoxItem = value as ComboBoxItem;
                if (comboBoxItem != null)
                {
                    string valueAsString = comboBoxItem.Content.ToString();
                    string parameterAsString = parameter as string;

                    if (parameterAsString != null)
                    {
                        if (parameterAsString == "VisibleOnYes")
                        {
                            if (valueAsString == "Yes")
                            {
                                return Visibility.Visible;
                            }
                        }

                        if (parameterAsString == "VisibleOnNo")
                        {
                            if (valueAsString == "No")
                            {
                                return Visibility.Visible;
                            }
                        }
                    }
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Please implement correct exception
            throw new NotImplementedException();
        }
    }
}
