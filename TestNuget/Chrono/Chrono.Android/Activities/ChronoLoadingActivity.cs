using Android.App;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using AndroidX.AppCompat.App;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Platform;
using Widgets;

namespace Chrono.Droid
{
    /// <summary>
    /// Chrono entry point for native activities.
    /// </summary>
    [Activity(LaunchMode = LaunchMode.SingleTop, Theme = "@style/ChronoAppTheme.ChronoEmptyActivity")]
    public class ChronoLoadingActivity : AppCompatActivity
    {
        /// <summary>
        /// Raises the create event.
        /// </summary>
        /// <param name="bundle">Bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            if (NIQThemeController.Theme == Widgets.Theme.NIQRebrand)
            {
                SetTheme(Resource.Style.ChronoRebrandAppTheme_ChronoEmptyActivity);
            }
            base.OnCreate(bundle);
            if (Build.VERSION.SdkInt != BuildVersionCodes.O && Build.VERSION.SdkInt != BuildVersionCodes.OMr1)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
            Platform.Init(this, bundle);
            SetContentView(Resource.Layout.ChronoLoadingActivity);
            
            var newIntent = new Intent(this.ApplicationContext, typeof(ChronoBasicActivity));
            newIntent.PutExtras(Intent);
            Device.BeginInvokeOnMainThread(() =>
            {
                StartActivity(newIntent);
            });
        }

        /// <summary>
        /// Raises On Pause event
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            Finish();
        }
    }
}