namespace Widgets
{
    /// <summary>
    /// Base class for NIQ applications
    /// </summary>
    public class NIQApplication : Application
    {
        #region Constants
        public const string OnResumeMessage = "com.mcoe.messages.OnResumeMessage";
        public const string OnSleepMessage = "com.mcoe.messages.OnSleepMessage";

        private const string InitialPageName = "Initial page";
        private const int InvalidIndex = -1;
        #endregion

        #region Properties
        /// <summary>
        /// Gets navigation stack of main page
        /// </summary>
        /// <returns>Page list, can be null</returns>
        private IReadOnlyList<Page> MainNavigationStack => MainPage?.Navigation.NavigationStack;

        /// <summary>
        /// Gets the modal pages count
        /// </summary>
        /// <returns>Returns 0 if main page is null otherwise count of modal pages</returns>
        private int ModalStackCount => MainPage?.Navigation.ModalStack.Count ?? 0;
        #endregion

        #region Private fields
        private Dictionary<int, string> transactionIds = new Dictionary<int, string>();
        private Page lastBackgroundPage = null;
        private List<int> backgroundPageHashes = new List<int>();
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQApplication"/> class
        /// </summary>
        public NIQApplication()
        {
            PageAppearing += (app, page) => OnPageAppearing(page);
            PageDisappearing += (app, page) => OnPageDisappearing(page);
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Called when the application starts
        /// </summary>
        protected override void OnStart()
        {
            var currentPageName = MainNavigationStack?.LastOrDefault()?.ToString() ?? InitialPageName;

            base.OnStart();
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application
        /// resumes from a sleeping state
        /// </summary>
        protected override void OnResume()
        {
            CustomMessagingCenter.Send(this, OnResumeMessage);

            base.OnResume();
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application
        /// enters the sleeping state
        /// </summary>
        protected override void OnSleep()
        {
            CustomMessagingCenter.Send(this, OnSleepMessage);
            base.OnSleep();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Called when page is appearing
        /// </summary>
        /// <param name="page">Page</param>
        private void OnPageAppearing(Page page)
        {
            LogForwardNavigation(page);
        }

        /// <summary>
        /// Called when page is disappearing
        /// </summary>
        /// <param name="page">Page</param>
        private void OnPageDisappearing(Page page)
        {
            LogBackwardNavigation(page);
        }

        /// <summary>
        /// Logs the forward navigation
        /// </summary>
        /// <param name="page">Page</param>
        private void LogForwardNavigation(Page page)
        {
            if (lastBackgroundPage != null &&
                !(MainNavigationStack?.Contains(lastBackgroundPage) ?? true))
            {
                var currentPageName = MainNavigationStack.LastOrDefault()?.ToString()
                    ?? InitialPageName;
                var previousPageHash = lastBackgroundPage.GetHashCode();

                if (transactionIds.ContainsKey(previousPageHash))
                {
                    transactionIds.Remove(previousPageHash);
                }
                RemoveBackgroundPageHash(lastBackgroundPage);
                lastBackgroundPage = null;
            }

            if (backgroundPageHashes.Contains(page.GetHashCode()))
            {
                RemoveBackgroundPageHash(page);
            }
            else if (MainNavigationStack?.Contains(page) ?? false)
            {
                int previousPageIndex;
                string previousPageName;
                int previousPageHash;
                if (page == MainNavigationStack.First())
                {
                    previousPageIndex = 0;
                    previousPageName = InitialPageName;
                    previousPageHash = InvalidIndex;
                }
                else
                {
                    previousPageIndex = MainNavigationStack.ToList()
                        .FindLastIndex(item => item == page) - 1;
                    previousPageName = MainNavigationStack[previousPageIndex]?.ToString()
                        ?? InitialPageName;
                    previousPageHash = MainNavigationStack[previousPageIndex]?.GetHashCode()
                        ?? InvalidIndex;
                }
            }
        }

        /// <summary>
        /// Logs the backward navigation
        /// </summary>
        /// <param name="page">Page</param>
        private void LogBackwardNavigation(Page page)
        {
            if (MainNavigationStack != null)
            {
                if (MainNavigationStack.Contains(page))
                {
                    var pageHash = page.GetHashCode();
                    lastBackgroundPage = page;
                    backgroundPageHashes.Add(pageHash);
                    LogBackgroundPage(lastBackgroundPage.ToString(), pageHash);
                }
                else if (MainNavigationStack.Any() && MainNavigationStack.Count == 1
                    && ModalStackCount == 0)
                {
                    var backgroundPageName = MainNavigationStack.LastOrDefault()?.ToString() ?? InitialPageName;
                    LogBackgroundPage(backgroundPageName, InvalidIndex);
                }

                if (MainNavigationStack.Count == 1)
                {
                    backgroundPageHashes.Clear();
                }
            }
        }

        /// <summary>
        /// Logs when page was moved to background
        /// </summary>
        /// <param name="page">Current page</param>
        /// <param name="pageHash">Hash of previous page</param>
        private void LogBackgroundPage(string page, int pageHash)
        {
            if (!transactionIds.ContainsKey(pageHash))
            {
                transactionIds.Add(pageHash, Guid.NewGuid().ToString());
            }
        }


        /// <summary>
        /// Gets page name
        /// </summary>
        /// <param name="fullPageName">Full page name</param>
        /// <returns>Short name page</returns>
        private string GetPageName(string fullPageName)
        {
            return !string.IsNullOrEmpty(fullPageName)
                ? fullPageName.Substring(fullPageName.LastIndexOf('.') + 1)
                : string.Empty;
        }

        /// <summary>
        /// Removes page hash from stack hash
        /// </summary>
        /// <param name="page"></param>
        private void RemoveBackgroundPageHash(Page page)
        {
            int pageHash = page.GetHashCode();
            if (backgroundPageHashes.Contains(pageHash))
            {
                backgroundPageHashes.Remove(pageHash);
            }
        }
        #endregion
    }
}
