namespace Widgets
{
    public enum PopSources
    {
        HardwareBackButton,
        SoftwareBackButton,
    }

    public class PopResult
    {
        public bool IsContinueHardwareButton { get; set; }
    }

    public class NIQNavigationPage : NavigationPage
    {
        #region Public members
        public static readonly BindableProperty IsEnableGestureBackForIOSProperty =
            BindableProperty.Create(
                nameof(IsEnableGestureBackForIOS),
                typeof(bool),
                typeof(NIQNavigationPage),
                true
            );
        public bool IsEnableGestureBackForIOS
        {
            get => (bool)GetValue(IsEnableGestureBackForIOSProperty);
            set => SetValue(IsEnableGestureBackForIOSProperty, value);
        }

        public bool IsRequestAppearing { get; private set; }
        #endregion

        #region Private members
        private int NavigationCount => Navigation.ModalStack.Count + Navigation.NavigationStack.Count;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="NIQNavigationPage"/>
        /// </summary>
        public NIQNavigationPage() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="NIQNavigationPage"/>
        /// </summary>
        /// <param name="root">Root page.</param>
        public NIQNavigationPage(Page root) : base(root)
        {
        }
        #endregion

        /// <summary>
        /// Catchs back button.
        /// </summary>
        /// <param name="popSource">Pop source.</param>
        /// <param name="popResult">Pop result.</param>
        /// <returns>The task.</returns>
        public virtual async Task CatchBackButton(PopSources popSource, PopResult popResult)
        {
            var page = GetDisplayPage();
            if (page == null)
            {
                return;
            }

            // Get implement interface. Priority is Page
            INavigationPopInterceptor interceptor = null;
            if (page is INavigationPopInterceptor pageInterceptor && pageInterceptor != null)
            {
                interceptor = pageInterceptor;
            }
            else if (page.BindingContext is INavigationPopInterceptor vmInterceptor && vmInterceptor != null)
            {
                interceptor = vmInterceptor;
            }

            // For iOS, we are ignore back button intercept for root page
            if (NavigationCount == 1 && Device.RuntimePlatform == Device.iOS)
            {
                return;
            }

            if (interceptor != null)
            {
                if (IsRequestAppearing)
                {
                    return;
                }

                IsRequestAppearing = true;
                bool res = await interceptor.RequestPop();
                IsRequestAppearing = false;

                if (!res)
                {
                    return;
                }

                if (NavigationCount == 1)
                {
                    popResult.IsContinueHardwareButton = true;
                }
                else
                {
                    await Pop(page, interceptor);
                }
            }
            else
            {
                await Pop(page, interceptor);
            }
        }

        #region Private methods
        /// <summary>
        /// Pops page.
        /// </summary>
        /// <param name="page">Page.</param>
        /// <param name="interceptor">Interceptor.</param>
        /// <returns>The task.</returns>
        private async Task Pop(Page page, INavigationPopInterceptor interceptor)
        {
            if (interceptor != null)
            {
                interceptor.IsPopRequest = true;
            }

            bool shouldBeClose = ((page is NIQDialog) && !(page as NIQDialog).IsNotCloseByBack) || !(page is NIQDialog);
            if (!IsModal(page))
            {
                await page.Navigation.PopAsync();
            }
            else if (shouldBeClose)
            {
                await page.Navigation.PopModalAsync();
            }

            if (interceptor != null)
            {
                interceptor.IsPopRequest = false;
            }
        }

        /// <summary>
        /// Gets displayed page.
        /// </summary>
        /// <returns>The page.</returns>
        private Page GetDisplayPage()
        {
            int modalCount = Navigation.ModalStack.Count;
            int navCount = Navigation.NavigationStack.Count;

            if (modalCount > 0)
            {
                return Navigation.ModalStack[modalCount - 1];
            }
            else if (navCount > 0)
            {
                return Navigation.NavigationStack[navCount - 1];
            }

            return null;
        }

        /// <summary>
        /// Checks if page is modal.
        /// </summary>
        /// <param name="page">Page to check.</param>
        /// <returns>Is modal or no.</returns>
        private bool IsModal(Page page)
        {
            return page.Navigation.ModalStack.FirstOrDefault(p => p == page) != null;
        }
        #endregion
    }

}
