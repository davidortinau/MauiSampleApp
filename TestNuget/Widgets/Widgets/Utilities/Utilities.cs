namespace Widgets
{
    public static class Utilities
    {
        /// <summary>
        /// Gets the default color
        /// </summary>
        public static Color DefaultColor = new(255, 0, 0, 0.5f);
        
        /// <summary>
        /// Gets activity indicator foreground color
        /// </summary>
        /// <returns>Color</returns>
        public static Color GetActivityIndicatorColor()
        {
            return NIQThemeController.Theme == Theme.Light
                ? NIQThemeController.Theme.Primary
                : NIQThemeController.Theme.Secondary;
        }

        /// <summary>
        /// Gets last page from navigation stack
        /// </summary>
        public static Page GetLastPage()
        {
            Page page = null;
            IReadOnlyList<Page> navigationStack = Application.Current?.MainPage?.Navigation.NavigationStack;
            if (navigationStack?.Any() ?? false)
            {
                page = navigationStack.Last();
            }
            if (page == null && Application.Current != null && Application.Current.MainPage != null)
            {
                page = Application.Current.MainPage;
            }
            return page;
        }

        /// <summary>
        /// Asynchronously adds a Page to the top of the navigation stack
        /// </summary>
        /// <param name="page">Page to be pushed on top of the navigation stack</param>
        /// <param name="animated">Whether to animate the push</param>
        /// <returns>A task that represents the asynchronous push operation</returns>
        public static async Task PushAsync(Page page, bool animated = false)
        {
            var initialPage = GetLastPage();
            if (initialPage != null)
            {
                await initialPage.Navigation.PushAsync(page, animated);
            }
        }
    }
}
