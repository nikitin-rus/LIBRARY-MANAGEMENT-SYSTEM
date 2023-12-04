using System;
using System.Globalization;
using System.Windows.Data;

namespace LIBRARY_MANAGEMENT_SYSTEM.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Type type = value.GetType();

                if (type == typeof(DateOnly))
                {
                    return ((DateOnly)value).ToString();
                }

                throw new Exception(
                    $"{nameof(DateConverter)}: Wrong value type:" +
                    $" {type.Name}. Expected: {targetType.Name}");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
