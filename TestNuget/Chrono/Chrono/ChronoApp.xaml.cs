using Widgets;
using WidgetsUtils = Widgets.Utilities;

namespace Chrono
{
    /// <summary>
    /// Chrono App for native invoke support
    /// </summary>
    public partial class ChronoApp : NIQApplication
    {
        /// <summary>
        /// Gets the navigation object
        /// </summary>
        public INavigation Navigation => WidgetsUtils.GetLastPage().Navigation;

        #region Constants
        private const string TAG = nameof(ChronoApp);
        #endregion

        /// <summary>
        /// Initializes the instance if <see cref="ChronoApp.cs"/>
        /// </summary>
        public ChronoApp(bool isEmptyApp = false)
        {
            InitializeComponent();

            NIQThemeController.InitTheme();
            if (!isEmptyApp)
            {
                MainPage = new ChronoPage();
            }
        }
    }
}