using System.Windows.Data;
using WPFSharp.Globalizer.Converters;

namespace WPFSharp.Globalizer
{
    public class LocalizationBinding : Binding
    {
        public LocalizationBinding()
        {
        }

        public LocalizationBinding(string path)
            : base(path)
        {
            Converter = new LocalizationConverter();
        }

        new public object FallbackValue
        {
            get { return base.FallbackValue; }
            set
            {
                base.FallbackValue = value;
                ConverterParameter = value;
            }
        }
    }
}
