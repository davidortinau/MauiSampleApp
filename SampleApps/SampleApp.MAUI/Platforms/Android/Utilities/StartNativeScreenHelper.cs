using Android.Content;

namespace SampleApp.MAUI.Platforms.Android
{
    public class StartNativeScreenHelper : IStartNativeScreenHelper
    {
        public void OpenNativeChronoScreen()
        {
            var ctx = Platform.CurrentActivity;
            var intent = new Intent(ctx, typeof(ChronoNative));
            ctx.StartActivity(intent);
        }
    }
}
