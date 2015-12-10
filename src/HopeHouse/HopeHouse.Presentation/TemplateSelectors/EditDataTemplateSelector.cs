using System.Windows;
using System.Windows.Controls;
using HopeHouse.Common.Interfaces;
using HopeHouse.Presentation.ViewModels;

namespace HopeHouse.Presentation.TemplateSelectors
{
    public class EditDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextBoxTemplate
        {
            get; set;
        }

        public DataTemplate CheckBoxTemplate
        {
            get; set;
        }

        public DataTemplate DisabledTemplate
        {
            get; set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var contentPresenter = container as ContentPresenter;
            if (contentPresenter != null)
            {
                DataGridCell cell = contentPresenter.Parent as DataGridCell;

                if (cell != null)
                {
                    if (((DataDisplayWrapper)cell.DataContext).Value is bool)
                    {
                        return CheckBoxTemplate;
                    }

                    if (((DataDisplayWrapper)cell.DataContext).Value is IDataProvider ||
                        ((DataDisplayWrapper)cell.DataContext).Value is IDataProviderAggregation)
                    {
                        return DisabledTemplate;
                    }
                }
            }

            return TextBoxTemplate;
        }
    }
}
