using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFSharp.Globalizer.Converters
{
    public class LocalizationConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return GlobalizedApplication.Instance.TryFindResource(value) ?? parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _LocalizationConverter ?? (_LocalizationConverter = new LocalizationConverter());
        } private static LocalizationConverter _LocalizationConverter;
    }
}
