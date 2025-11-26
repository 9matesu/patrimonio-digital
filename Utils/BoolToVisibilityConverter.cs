using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace patrimonio_digital.Utils
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = (bool)(value ?? false);
            if (Invert)
                flag = !flag;
            return flag ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            bool flag = visibility == Visibility.Visible;
            if (Invert)
                flag = !flag;
            return flag;
        }
    }
}