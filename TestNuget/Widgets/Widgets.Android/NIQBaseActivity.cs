using Android.OS;
using Android.Runtime;
using Android.App;
using Android.Content;
using Microsoft.Maui.Embedding;

namespace Widgets.Droid
{
    /// <summary>
    /// Base activity class
    /// </summary>
    public class NIQBaseActivity : MauiAppCompatActivity
    {
        public event NIQBackButtonPressedEventHandler BackPressedEvent;
        public delegate Task NIQBackButtonPressedEventHandler(PopSources popSource, PopResult popResult);

        #region Properties
        /// <summary>
        /// ImageId identifier
        /// </summary>
        public static readonly int PickImageId = 1000;

        /// <summary>
        /// Task completion source
        /// </summary>
        public TaskCompletionSource<Android.Net.Uri> ImageTaskCompletionSource { set; get; }

        /// <summary>
        /// base activity instance
        /// </summary>
        public static NIQBaseActivity Instance { get; private set; }

        /// <summary>
        /// Is main activity flag
        /// </summary>
        public virtual bool IsMainActivity => true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is resume.
        /// </summary>
        /// <value><c>true</c> if this instance is resume; otherwise, <c>false</c>.</value>
        protected bool IsResume { get; private set; }
        #endregion

        public MauiContext mauiContext;

        #region Private fields
        private Handler handler = new Handler();
        #endregion

        #region Overridables
        /// <summary>
        /// Called when activity is launching
        /// </summary>
        /// <param name="savedInstanceState">Bundle</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            MauiApp mauiApp = builder.Build();
            mauiContext = new MauiContext(mauiApp.Services, this);
        }

        /// <summary>
        /// Called as part of the activity lifecycle when an activity is going into
        ///  the background, but has not (yet) been killed.
        /// </summary>
        protected override void OnPause()
        {
            IsResume = false;
            base.OnPause();
        }

        /// <summary>
        /// Raises the resume event.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            IsResume = true;
        }

        /// <summary>
        /// Called when activity is closing
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        /// <summary>
        /// Called when there is result after permission request
        /// </summary>
        /// <param name="requestCode">Passed request code</param>
        /// <param name="permissions">Requested permission</param>
        /// <param name="grantResults">Grant result</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// Handles user interaction event
        /// </summary>
        public override void OnUserInteraction()
        {
            base.OnUserInteraction();
        }

        /// <summary>
        /// Called when result is ready
        /// </summary>
        /// <param name="requestCode">request code</param>
        /// <param name="resultCode">result code</param>
        /// <param name="intent">intent</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);
            if (resultCode == Result.Ok && requestCode == PickImageId)
            {
                Android.Net.Uri uri = intent.Data;
                ImageTaskCompletionSource.SetResult(uri);
            }
        }

        /// <summary>
        /// On back pressed.
        /// </summary>
        public async override void OnBackPressed()
        {
            if (BackPressedEvent == null)
            {
                base.OnBackPressed();
            }
            else
            {
                var popResult = new PopResult();
                await BackPressedEvent(PopSources.HardwareBackButton, new PopResult());

                // When is root page
                if (popResult.IsContinueHardwareButton)
                {
                    base.OnBackPressed();
                }
            }
        }

        /// <summary>
        /// Handles Trim Memory notification
        /// </summary>
        /// <param name="level">Trim memory level</param>
        public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {
            base.OnTrimMemory(level);
        }
        #endregion
    }
}