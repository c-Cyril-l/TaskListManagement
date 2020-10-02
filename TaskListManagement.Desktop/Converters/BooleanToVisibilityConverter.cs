using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TaskListManagement.Desktop.Converters
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        #region Overrides of MarkupExtension

        private BooleanToVisibilityConverter _booleanConverter;
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _booleanConverter ??= new BooleanToVisibilityConverter();
        }

        #endregion

        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isVisible = System.Convert.ToBoolean(value);

            return isVisible ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }

        #endregion
    }
}