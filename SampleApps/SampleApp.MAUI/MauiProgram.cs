using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Widgets.Droid.Renderers;
using Widgets;
using Widgets.Droid;
using Chrono.Droid;

namespace SampleApp.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder()
                .UseMauiApp<App>() // since none of the methods called this first
                .UseMauiCompatibility()
                .UseSharedMauiApp() // from Widgets, registers some fonts
                .UseSharedDroidMauiApp() // from Widgets.Android, registers renderers and effects
                .UseChronoSharedDroidMauiApp() // from Chrono.Droid, registers a renderer
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            HandlerOverrides();
            
            return builder.Build();
        }

        private static void HandlerOverrides()
        {
            Microsoft.Maui.Handlers.ScrollViewHandler.Mapper.AppendToMapping("VerticalScrollBarVisibility",
                (handler, view) =>
                {
                    if (view.VerticalScrollBarVisibility == ScrollBarVisibility.Always)
                    {
                        handler.PlatformView.ScrollbarFadingEnabled = false;
                    }
                });
        }
    }
}