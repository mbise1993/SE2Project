using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HopeHouse.Presentation.Converters
{
    public class EducationToVisibilityConverter : IValueConverter
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
                        if (parameterAsString == "VisibleOnDroppedOut")
                        {
                            if (valueAsString == "Dropped out")
                            {
                                return Visibility.Visible;
                            }
                        }

                        if (parameterAsString == "VisibleOnOther")
                        {
                            if (valueAsString == "Other")
                            {
                                return Visibility.Visible;
                            }
                        }

                        if (parameterAsString == "VisibleOnInSchool")
                        {
                            if ((valueAsString == "In highschool") ||
                                (valueAsString == "In GED program") ||
                                (valueAsString == "In college") ||
                                (valueAsString == "Enrolled but not started"))
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
            //Please implement correct handling of exception
            throw new NotImplementedException();
        }
    }
}
