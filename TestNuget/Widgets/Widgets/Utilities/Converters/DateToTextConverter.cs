using System.Globalization;

namespace Widgets
{
    /// <summary>
    /// Date to text converter
    /// </summary>
    class DateToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length > 1 && values[0] is DateTime date)
            {
                var format = (string)values[1];
                return date.ToString(format, CultureInfo.CurrentCulture);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var result = new object[2];
            var strDate = (string)value;
            result[0] = DateTime.TryParse(strDate, out DateTime date)
                ? date
                : Binding.DoNothing;
            result[1] = Binding.DoNothing;
            
            return result;
        }
    }
}
