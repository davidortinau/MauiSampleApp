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
                .UseMauiApp<App>()
                .UseMauiCompatibility()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers((handlers) =>
                {
                
#if ANDROID
                handlers.AddCompatibilityRenderer(typeof(Editor), typeof(NIQEditorRenderer));
                handlers.AddCompatibilityRenderer(typeof(NIQWebView), typeof(NIQWebViewRenderer));
                handlers.AddCompatibilityRenderer(typeof(NIQCarouselView), typeof(NIQCarouselRenderer));
                handlers.AddCompatibilityRenderer(typeof(Chrono.ChronoListView), typeof(Chrono.Droid.ChronoListViewRenderer));
                handlers.AddCompatibilityRenderer(typeof(Widgets.NIQDarkPopupItem), typeof(NIQDarkPopupItemRenderer));
                //handlers.AddCompatibilityRenderer(typeof(ScrollView), typeof(NIQScrollViewRenderer));
#endif
                })
                .ConfigureEffects(effects =>
                {
#if ANDROID
                    effects.Add<NIQButtonTintEffect, NIQAndroidButtonTintEffect>();
                    effects.Add<TintImage, TintImageImpl>();
#endif
                });

 /*           });

            builder.UseSharedMauiApp();

#if ANDROID
            builder.UseSharedDroidMauiApp();
            builder.UseChronoSharedDroidMauiApp();
#endif
*/

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}