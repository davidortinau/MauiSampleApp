using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using AndroidX.AppCompat.App;
using Newtonsoft.Json;
using Widgets;
using Microsoft.Maui.Hosting;
using Microsoft.Maui;
using Microsoft.Maui.Embedding;

namespace Chrono.Droid
{
    /// <summary>
    /// Entry point for show chrono dialogs on top of native activities
    /// </summary>
    [Activity(LaunchMode = LaunchMode.SingleTop, Theme = "@style/ChronoAppTheme")]
    public class ChronoDialogActivity : AppCompatActivity
    {
        #region Constants
        private const string TAG = nameof(ChronoDialogActivity);

        public const string ExtraDialogMode = "ChronoDialogActivity.DialogMode";

        public const string ExtraKeyAuditInfo = "ChronoDialogActivity.AuditInfo";

        public const string ExtraKeyInactivityDateTime = "ChronoDialogActivity.InactivityDateTime";

        public const string ExtraKeyMandatorySync = "ChronoDialogActivity.MandatorySync";
        #endregion

        MauiContext mauiContext;

        public enum ChronoShowDialogMode : int
        {
            Inactivity,
            Break,
            Custom,
            Chrono
        }

        #region Overridables
        /// <summary>
        /// Raises the create event.
        /// </summary>
        /// <param name="savedInstanceState">Saved Instance State.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (NIQThemeController.Theme == Widgets.Theme.NIQRebrand)
            {
                SetTheme(Resource.Style.ChronoRebrandAppTheme);
            }
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt != BuildVersionCodes.O && Build.VERSION.SdkInt != BuildVersionCodes.OMr1)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            MauiApp mauiApp = builder.Build();
            mauiContext = new MauiContext(mauiApp.Services, this);

            SetContentView(Resource.Layout.ChronoDialogActivity);
            ChronoShowDialogMode dialogMode = (ChronoShowDialogMode)Intent.GetIntExtra(ExtraDialogMode, 0);

            switch (dialogMode)
            {
                case ChronoShowDialogMode.Inactivity:
                    var inactivityStartTime = DateTime.Parse(Intent.GetStringExtra(ExtraKeyInactivityDateTime));
                    var auditInfo = JsonConvert.DeserializeObject<ChronoAuditInfo>(Intent.GetStringExtra(ExtraKeyAuditInfo));
                    var dialogPage = ChronoDialogHelper.ShowLongInactivyDialog(auditInfo, inactivityStartTime, ChronoNativeDialogsBridge.SelectedAction, true);
                    AndroidX.Fragment.App.Fragment chronoPageFragment = dialogPage.CreateSupportFragment(mauiContext);
                    SupportFragmentManager
                        .BeginTransaction()
                        .Replace(Resource.Id.fragmentPlaceholder, chronoPageFragment)
                        .Commit();
                    break;
                case ChronoShowDialogMode.Custom:
                    ShowCustomDialog();
                    break;
                case ChronoShowDialogMode.Break:
                    break;
            }
        }

        /// <summary>
        /// Raises On Back Pressed event
        /// </summary>
        public override void OnBackPressed()
        {
            //Do nothing
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
        #endregion

        #region Public methods
        /// <summary>
        /// Closes Dialog Fragment
        /// </summary>
        public void CloseDialogFragment()
        {
            FindViewById<FrameLayout>(Resource.Id.secondFragmentPlaceholder).Visibility = ViewStates.Gone;
        }

        /// <summary>
        /// Shows activity indicator
        /// </summary>
        public void ShowActivityIndicator()
        {
            FindViewById<Android.Widget.ProgressBar>(Resource.Id.activityIndicator).Visibility = ViewStates.Visible;
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Shows custom dialog
        /// </summary>
        private void ShowCustomDialog()
        {
            AndroidX.Fragment.App.Fragment chronoPageFragment = ChronoNativeDialogsBridge.ChronoDialog.CreateSupportFragment(mauiContext);
            SupportFragmentManager
                        .BeginTransaction()
                        .Replace(Resource.Id.fragmentPlaceholder, chronoPageFragment)
                        .Commit();
        }
        #endregion
    }
}