namespace Widgets
{
    /// <summary>
    /// Theme App for native invoke support
    /// </summary>
    public partial class ThemeApp : NIQApplication
    {
        #region Constants
        private const string TAG = nameof(ThemeApp);
        #endregion

        /// <summary>
        /// Initializes the instance if <see cref="ThemeApp.cs"/>
        /// </summary>
        public ThemeApp()
        {
            InitializeComponent();
            NIQThemeController.InitTheme();
        }
    }
}
