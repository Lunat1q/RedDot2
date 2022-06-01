using System;
using System.Windows.Data;

namespace RD2.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanToNameConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(object) && targetType != typeof(string))
                throw new InvalidOperationException("The target must be a object or string");

            var paramString = parameter as string;
            var paramOptions = paramString?.Split('|');
            if (paramOptions == null || paramOptions.Length != 2)
            {
                throw new InvalidOperationException("Parameter is not set correctly, use | as a separator and provide 2 values");
            }

            return value != null && (bool)value ? paramOptions[0] : paramOptions[1];
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}