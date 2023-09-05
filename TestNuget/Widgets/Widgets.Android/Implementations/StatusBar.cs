using Android.OS;

namespace Widgets.Droid
{
    public class StatusBar : IStatusBar
    {
        /// <summary>
        /// Updates status bar color
        /// </summary>
        /// <param name="color">The color</param>
        public void UpdateStatusBarColor(Color color)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                var androidColor = Utilities.ParseColor(color.AddLuminosity((float)-0.1));
                Platform.CurrentActivity?.Window.SetStatusBarColor(androidColor);
            }
        }
    }
}