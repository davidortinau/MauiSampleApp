using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Hosting;

namespace Chrono.Droid
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseChronoSharedDroidMauiApp(this MauiAppBuilder builder)
        {
            builder
                .ConfigureMauiHandlers((handlers) =>
                {
                    handlers.AddHandler(typeof(Chrono.ChronoListView), typeof(ChronoListViewRenderer));
                });
            return builder;
        }
    }
}
