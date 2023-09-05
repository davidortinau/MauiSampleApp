using Microsoft.Maui.Controls.Compatibility.Hosting;
using Widgets.Droid.Renderers;

namespace Widgets.Droid
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedDroidMauiApp(this MauiAppBuilder builder)
        {
            builder
                .ConfigureMauiHandlers((handlers) =>
                {
                    //handlers.AddHandler(typeof(NIQPopupItem), typeof(NIQPopupItemRenderer));
                    handlers.AddCompatibilityRenderer(typeof(Editor), typeof(NIQEditorRenderer));
                    handlers.AddCompatibilityRenderer(typeof(NIQWebView), typeof(NIQWebViewRenderer));
                    handlers.AddCompatibilityRenderer(typeof(NIQCarouselView), typeof(NIQCarouselRenderer));
                    handlers.AddCompatibilityRenderer(typeof(NIQDarkPopupItem), typeof(NIQDarkPopupItemRenderer));
                })
                .ConfigureEffects(effects =>
                {
                    effects.Add<NIQButtonTintEffect, NIQAndroidButtonTintEffect>();
                    effects.Add<TintImage, TintImageImpl>();

                });
            return builder;
        }
    }
}
