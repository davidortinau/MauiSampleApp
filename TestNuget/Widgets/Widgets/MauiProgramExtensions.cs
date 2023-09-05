namespace Widgets
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
            builder
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("aktivgrotesk_regular.ttf", "AktivGrotesk");
                    fonts.AddFont("aktivgrotesk_bold.ttf", "AktivGroteskBold");
                    fonts.AddFont("aktivgrotesk_italic.ttf", "AktivGroteskItalic");
                });

            return builder;
        }
    }
}
