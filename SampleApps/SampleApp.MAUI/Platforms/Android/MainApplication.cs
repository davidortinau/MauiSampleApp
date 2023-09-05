using Android.App;
using Android.Runtime;
using Widgets.Droid;
using SampleApp.MAUI.Platforms.Android;
using Chrono.Droid;
using Chrono.Utilities;
using Chrono;
using Authority.Droid;

namespace SampleApp.MAUI
{
    [Application]
    public class MainApplication : NIQApplicationDroid
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        public override MauiApp BuildMauiApp()
        {
            RegisterDependencyService.Register();
            DependencyService.Register<IStartNativeScreenHelper, StartNativeScreenHelper>();
            DependencyService.Register<IChronoNativeHelper, ChronoNativeHelper>();
            DependencyService.Register<IChronoApplicationObserver, ChronoApplicationObserver>();
            return MauiProgram.CreateMauiApp();
        }
    }
}