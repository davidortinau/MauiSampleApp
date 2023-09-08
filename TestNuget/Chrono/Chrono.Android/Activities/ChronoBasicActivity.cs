using Android.App;
using Android.OS;
using Android.Content;
using Android.Content.PM;
using Widgets.Droid;
using System;
using Android.Util;
using Android.Views;
using AndroidX.AppCompat.App;
using Microsoft.Maui;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Platform;
using Newtonsoft.Json;

namespace Chrono.Droid
{
    /// <summary>
    /// Chrono entry point for native activities.
    /// </summary>
    [Activity(LaunchMode = LaunchMode.SingleTop, Theme = "@style/ChronoAppTheme.ChronoEmptyActivity")]
    public class ChronoBasicActivity : AppCompatActivity
    {
        #region Constants
        private const string TAG = nameof(ChronoBasicActivity);

        public const string ExtraKeySelectedDate = "ChronoNativeHelper.SelectedDate";

        public const string ExtraKeyInactivityDateTime = "ChronoNativeHelper.InactivityDateTime";

        public const string ExtraKeyAuditInfo = "ChronoNativeHelper.AuditInfo";

        public const string ExtraKeyChronoInactivityActionType = "ChronoNativeHelper.InactivityActionType";

        public const string ExtraKeyChronoBreakStart = "ChronoNativeHelper.ChronoBreakStart";

        public const string ExtraKeyChronoActivityMode = "ChronoNativeHelper.ActivityMode";
        #endregion

        public enum ChronoActivityMode : int 
        {
            Inactivity,
            Break,
            SelectedDate,
            Calendar
        }

        /// <summary>
        /// Raises the create event.
        /// </summary>
        /// <param name="bundle">Bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            if (Build.VERSION.SdkInt != BuildVersionCodes.O && Build.VERSION.SdkInt != BuildVersionCodes.OMr1)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
            ChronoActivityMode activityMode = (ChronoActivityMode)Intent.GetIntExtra(ExtraKeyChronoActivityMode, (int)ChronoActivityMode.Calendar);

            switch (activityMode)
            {
                case ChronoActivityMode.Inactivity:
                    var inactivityStartTime = DateTime.Parse(Intent.GetStringExtra(ExtraKeyInactivityDateTime));
                    var auditInfo = JsonConvert.DeserializeObject<ChronoAuditInfo>(Intent.GetStringExtra(ExtraKeyAuditInfo));
                    var actionType = Intent.GetIntExtra(ExtraKeyChronoInactivityActionType, 0);
                    break;
                case ChronoActivityMode.Break:
                    var breakStartTime = DateTime.Parse(Intent.GetStringExtra(ExtraKeyChronoBreakStart));
                    break;
                case ChronoActivityMode.SelectedDate:
                    var selectedDate = DateTime.Parse(Intent.GetStringExtra(ExtraKeySelectedDate));
                    break;
                case ChronoActivityMode.Calendar:
                    MauiAppBuilder builder = MauiApp.CreateBuilder();
                    builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
                    MauiApp mauiApp = builder.Build();
                    var mauiContext = new MauiContext(mauiApp.Services, this);
                    SetContentView(new ChronoPage().ToContainerView(mauiContext));
                    break;
            }
        }


        /// <summary>
        /// Application developers override this method to perform actions when the application
        /// resumes from a sleeping state
        /// </summary>
        protected override void OnResume()
        {
            ChronoManager.Instance.IsChronoOnTop = true;
            base.OnResume();
        }

        /// <summary>
        /// Called as part of the activity lifecycle when an activity is going into
        ///  the background, but has not (yet) been killed.
        /// </summary>
        protected override void OnPause()
        {
            ChronoManager.Instance.IsChronoOnTop = false;
            base.OnPause();
        }
    }
}