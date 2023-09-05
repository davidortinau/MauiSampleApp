using Utilities;
using Widgets;

namespace SampleApp.MAUI
{
    public partial class App : NIQApplication
    {
        public App()
        {
            InitializeComponent();

            NIQThemeController.Theme = Theme.NIQRebrand;
            MainPage = new MainPage();
        }
    }
}