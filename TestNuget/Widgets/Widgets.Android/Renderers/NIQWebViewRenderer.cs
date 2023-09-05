using Android.Content;
using WebView = Android.Webkit.WebView;
using Widgets;
using Webkit = Android.Webkit;
using Widgets.Droid;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

[assembly: ExportRenderer(typeof(NIQWebView), typeof(NIQWebViewRenderer))]
namespace Widgets.Droid
{
    public class NIQWebViewRenderer : WebViewRenderer
    {
        #region Construction
        /// <summary>
        /// Initializes a new instance of the NIQWebViewRenderer
        /// </summary>
        /// <param name="context">Context.</param>
        public NIQWebViewRenderer(Context context) : base(context)
        {

        }
        #endregion

        /// <summary>
        /// Terms of use web view custom client.
        /// </summary>
        public class CustomWebViewClient : Webkit.WebViewClient
        {
            #region Private members
            private NIQWebView _xwebView;
            #endregion

            /// <summary>
            /// Custom web view client
            /// </summary>
            /// <param name="customWebView">Custom web view</param>
            public CustomWebViewClient(NIQWebView xwebView)
            {
                _xwebView = xwebView;
            }

            /// <summary>
            /// Notify the host application that a page has finished loading.
            /// </summary>
            /// <param name="view">View.</param>
            /// <param name="url">URL.</param>
            public override async void OnPageFinished(WebView view, string url)
            {
                if (_xwebView != null)
                {
                    view.Settings.JavaScriptEnabled = true;
                    string result = await _xwebView.EvaluateJavaScriptAsync("(function(){return document.body.scrollHeight;})()");
                    _xwebView.HeightRequest = Convert.ToDouble(result);
                }
                base.OnPageFinished(view, url);
            }

            /// <summary>
            /// Overrides url loading action
            /// </summary>
            /// <param name="view">Web view</param>
            /// <param name="url">Url</param>
            /// <returns><c>true</c> if it's needed to open the url in the webview; otherwise, <c>false</c>.</returns>
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                try
                {
                    Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception ex)
                {
                }
                return true;
            }
        }

        /// <summary>
        /// Notify the host application that an element is changed
        /// </summary>
        /// <param name="e">Event.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Microsoft.Maui.Controls.WebView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement is NIQWebView xwebView)
            {
                if (e.OldElement == null)
                {
                    Control.SetWebViewClient(new CustomWebViewClient(xwebView));
                }
            }
        }
    }
}
