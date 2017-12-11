using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;


namespace WpfApplication3.Converters
{
    public class MaskedDecimalConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (decimal?)General.ExtractNumeric(value?.ToString());
        }
    }
}
