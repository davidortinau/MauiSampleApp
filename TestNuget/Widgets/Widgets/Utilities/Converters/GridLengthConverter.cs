using System.Globalization;

namespace Widgets
{
    /// <summary>
    /// Grid row height or column width converter
    /// </summary>
    public class GridLengthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                if (string.IsNullOrEmpty(stringValue))
                {
                    return 0;
                }
                else
                {
                    return new GridLength(2, GridUnitType.Star);
                }
            }
            return "*";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
