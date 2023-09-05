using System.Globalization;

namespace Widgets
{
    /// <summary>
    /// Button text value converter
    /// </summary>
    public class ButtonTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null)
            {
                string text = (string)values[0];

                bool upperCaseAllowed = true;
                if (values.Length > 1 && values[1] is bool paramValue)
                {
                    upperCaseAllowed = paramValue;
                }

                return upperCaseAllowed && NIQThemeController.Theme == Theme.Light
                    ? text?.ToUpper()
                    : text;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
