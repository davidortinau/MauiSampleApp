using System.ComponentModel;
using System.Windows.Input;

namespace Widgets
{
    class NIQDocumentAcceptanceViewModel : INotifyPropertyChanged
    {
        private bool isAcceptedVisible;
        private bool isIndicatorVisible = false;
        private HtmlWebViewSource webViewSource;
        private Page page;

        #region Properties
        /// <summary>
        /// OnAccepted action
        /// </summary>
        public Action OnAccepted { get; set; }

        /// <summary>
        /// Is document accepted
        /// </summary>
        public bool IsAccepted { get; set; }

        /// <summary>
        /// Is accepted button visible
        /// </summary>
        public bool IsAcceptedVisible
        {
            get => isAcceptedVisible;
            set
            {
                if (IsAcceptedVisible != value)
                {
                    isAcceptedVisible = value;
                    OnPropertyChanged(nameof(IsAcceptedVisible));
                }
            }
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Html WebView source
        /// </summary>
        public HtmlWebViewSource WebViewSource
        {
            get => webViewSource;
            set
            {
                if (webViewSource != value)
                {
                    value.Html =
                        "<script type='text/javascript'>function init() {" +
                            "window.onscroll = function(ev)" +
                            "{" +
                                "if ((window.innerHeight + window.pageYOffset) >= document.body.offsetHeight)" +
                                "{" +
                                    "window.location.hash = 'bottom';" +
                                    "location.reload();" +
                                "}" +
                            "};" +
                        "}</script>" +
                         "<html>" +
                             "<body onload='init()'>" +
                             $"{value.Html}" +
                             "</body>" +
                         "</html>";


                    webViewSource = value;
                    OnPropertyChanged(nameof(WebViewSource));
                }
            }
        }

        /// <summary>
        /// AcceptCommand
        /// </summary>
        public ICommand AcceptCommand { get; }

        /// <summary>
        /// PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets property to show/hide Progress Indicator
        /// </summary>
        private bool IsIndicatorVisible
        {
            get => isIndicatorVisible;
            set
            {
                isIndicatorVisible = value;
                OnPropertyChanged(nameof(IsIndicatorVisible));
            }
        }
        #endregion

        /// <summary>
        /// NIQDocumentAcceptanceViewModel
        /// </summary>
        /// <param name="page"></param>
        public NIQDocumentAcceptanceViewModel(Page page)
        {
            AcceptCommand = new Command(AcceptTermsButtonClick);
            this.page = page;
        }

        #region Private methods
        /// <summary>
        /// Is called to notify that property was changed.
        /// </summary>
        /// <param name="propertyName">The property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// acceptTerms button is clicked
        /// </summary>
        private async void AcceptTermsButtonClick()
        {
            await page.Navigation.PopModalAsync();
            OnAccepted?.Invoke();
        }
        #endregion

        /// <summary>
        /// Called when the user scrolls scroll view
        /// </summary>
        public void ScrollViewScrolled()
        {
            IsAcceptedVisible = !IsAccepted;
        }

        /// <summary>
        /// Loads HTML content
        /// </summary>
        /// <param name="getContentFunc">Function to get content</param>
        public async void LoadContent(Func<string> getContentFunc)
        {
            if (getContentFunc != null)
            {
                IsIndicatorVisible = true;

                string content = await Task.Run(() => getContentFunc());

                WebViewSource = new HtmlWebViewSource()
                {
                    Html = content
                };

                IsIndicatorVisible = false;
            }
        }
    }
}