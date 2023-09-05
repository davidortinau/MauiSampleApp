using System.Globalization;

namespace Widgets
{
    /// <summary>
    /// Time to text converter
    /// </summary>
    class TimeToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length > 1 && values[0] is TimeSpan time)
            {
                var format = (string)values[1];
                return time.ToString(format, culture);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var result = new object[2];
            var strDate = (string)value;
            result[0] = TimeSpan.TryParse(strDate, out TimeSpan time)
                ? time
                : Binding.DoNothing;
            result[1] = Binding.DoNothing;
            return result;
        }
    }
}
