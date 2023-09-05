namespace Widgets
{
    /// <summary>
    /// Navigation extensions
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Removes specified page from modal stack
        /// </summary>
        /// <param name="page">Page to remove</param>
        public static async Task RemoveModalPage(this INavigation navigation, Page page)
        {
            if (!navigation.ModalStack.Contains(page))
            {
                return;
            }

            IEnumerable<Page> modalStack = navigation.ModalStack;

            while (navigation.ModalStack.Any())
            {
                await navigation.PopModalAsync(animated: false);
            }

            foreach (Page oldPage in modalStack)
            {
                if (oldPage != page)
                {
                    await navigation.PushModalAsync(oldPage, animated: false);
                }
            }
        }
    }
}
