using Microsoft.Maui.Controls.Compatibility.Platform.Android;

namespace Widgets.Droid
{
    public class Utilities
    {
        /// <summary>
        /// Parses Xamarin.Forms.Color to Android.Graphics.Color
        /// </summary>
        /// <param name="xfColor">Color to cast</param>
        /// <returns>Android color</returns>
        public static Android.Graphics.Color ParseColor(Color xfColor)
        {
            return ColorExtensions.ToAndroid(xfColor);
        }
    }
}