using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace XiDeng.Common.Converts
{
    public class BoolToShowTriangleSwitchConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Show Wheel" : "Show Triangle";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Show Wheel";
        }
    }
}
