using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Chrono;
using Widgets;
using Button = Android.Widget.Button;

namespace SampleApp.MAUI.Platforms.Android
{
    [Activity(Label = "ChronoNative", Theme = "@style/CustomTheme")]
    public class ChronoNative : AppCompatActivity
    {
        private bool isUnexportCheckPerformed = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ChronoNative);

            FindViewById<Button>(Resource.Id.open_chrono_button).Click += (sender, args) =>
            {
                OpenChrono();
            };
            FindViewById<Button>(Resource.Id.show_atypical_button).Click += (sender, args) =>
            {
                ShowLongInactivityDialog();
            };
            
            var toolbar = FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Native Implementation";

            ChronoManager.Instance.SetNativeApplicationType(true);
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        private void AddSystemActivity()
        {
        }

        private void OpenChrono()
        {
            ChronoManager.Instance.StartChronoFromNative();
        }

        private void ShowLongInactivityDialog()
        {
            ChronoManager.Instance.ShowLongInactivityDialogNative(null, DateTime.Now, (action) =>
            {
                Toast.MakeText(this, action.ToString(), ToastLength.Long);
            });
        }
    }
}