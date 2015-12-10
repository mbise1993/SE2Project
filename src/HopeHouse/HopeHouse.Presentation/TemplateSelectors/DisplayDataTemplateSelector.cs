using System.Windows;
using System.Windows.Controls;
using HopeHouse.Common.Interfaces;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Presentation.TemplateSelectors
{
    public class DisplayDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate
        {
            get; set;
        }

        public DataTemplate NullOrEmptyTemplate
        {
            get; set;
        }

        public DataTemplate BooleanTemplate
        {
            get; set;
        }

        public DataTemplate ViewableObjectTemplate
        {
            get; set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contentPresenter = container as ContentPresenter;
            if (contentPresenter != null)
            {
                DataGridCell cell = contentPresenter.Parent as DataGridCell;
                object value = ((DataDisplayWrapper)cell.DataContext).Value;

                if (cell != null)
                {
                    if(value == null)
                    {
                        return NullOrEmptyTemplate;
                    }

                    if (value is string && string.IsNullOrEmpty((string)value))
                    {
                        return NullOrEmptyTemplate;
                    }

                    if (value is bool)
                    {
                        return BooleanTemplate;
                    }

                    if (value is IDataProvider || value is IDataProviderAggregation)
                    {
                        return ViewableObjectTemplate;
                    }
                }
            }

            return DefaultTemplate;
        }
    }
}
