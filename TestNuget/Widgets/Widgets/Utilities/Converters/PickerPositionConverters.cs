using System.Globalization;

namespace Widgets
{
    /// <summary>
    /// Picker row converter
    /// </summary>
    public class PickerRowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int row = 0;
            if (values != null && values.Length == 2)
            {
                if (values[0] != null && values[0] is int index
                    && values[1] != null && values[1] is int itemsInRow && itemsInRow != 0)
                {
                    row = index / itemsInRow;
                }
            }
            return row;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    /// <summary>
    /// Picker column converter
    /// </summary>
    public class PickerColumnConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int column = 0;
            if (values != null && values.Length == 2)
            {
                if (values[0] != null && values[0] is int index
                    && values[1] != null && values[1] is int itemsInRow && itemsInRow != 0)
                {
                    column = index % itemsInRow;
                }
            }
            return column;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
