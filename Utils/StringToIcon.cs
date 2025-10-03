using System;
using System.Globalization;
using System.Windows.Data;
using FontAwesome.WPF;

// conversão de string pro formato lido pelo fontawesome 

namespace patrimonio_digital.Utils
{
    public class StringToIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && Enum.TryParse(typeof(FontAwesomeIcon), str, out var icon))
                return icon;
            return FontAwesomeIcon.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

    }
}
