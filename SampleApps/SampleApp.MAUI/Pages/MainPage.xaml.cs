using System.Windows.Input;
using Widgets;
using Utilities;

namespace SampleApp.MAUI
{
    public partial class MainPage : Shell
    {
        public ICommand OpenMauiControllerCommand { get; set; }
        public ICommand OpenMauiInNativeCommand { get; set; }

        public bool IsLightTheme => NIQThemeController.Theme == Theme.NIQLight;
        public Color PrimaryColor => IsLightTheme ?
            NIQThemeController.Theme.Background : NIQThemeController.Theme.Primary;

        private string appDirPath;

        public MainPage()
        {
            InitializeComponent();
            OpenMauiControllerCommand = new Command(OpenPage<ChronoPage>());
            OpenMauiInNativeCommand = new Command(OpenNativeChronoActivity);

            BindingContext = this;
        }

        /// <summary>
        /// Opens page
        /// </summary>
        private Action OpenPage<T>()
            where T : new()
        {
            return async () =>
            {
                await Navigation.PushAsync(new T() as Page);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            if (Shell.Current != null && Shell.Current.FlyoutIsPresented)
            {
                Shell.Current.FlyoutIsPresented = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }

        private void OpenNativeChronoActivity()
        {
            DependencyService.Get<IStartNativeScreenHelper>()
               .OpenNativeChronoScreen();
        }
    }
}
