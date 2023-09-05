using System.Globalization;

namespace Widgets
{
    /// <summary>
    /// Inverse boolean converter
    /// </summary>
    public class InverseBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts boolean
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="targetType">The type of object</param>
        /// <param name="parameter">Use during the conversion</param>
        /// <param name="culture">Culture info</param>
        /// <returns>Converted object</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolValue ? !boolValue : value;
        }

        /// <summary>
        /// Converts back boolean
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="targetType">The type of object</param>
        /// <param name="parameter">Use during the conversion</param>
        /// <param name="culture">Culture info</param>
        /// <returns>Converted back object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
