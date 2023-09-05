namespace Widgets
{
    public class NIQWebView : WebView
    {
        public static readonly BindableProperty UrlProperty = BindableProperty.Create(
            propertyName: "Url", returnType: typeof(string),
            declaringType: typeof(NIQWebView), defaultValue: default(string));

        public static BindableProperty EvaluateJavascriptProperty = BindableProperty.Create(
            nameof(EvaluateJavascript), typeof(Func<string, Task<string>>), typeof(NIQWebView),
            null, BindingMode.OneWayToSource);

        public string Url
        {
            get => (string)GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }

        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return (Func<string, Task<string>>)this.GetValue(EvaluateJavascriptProperty); }
            set { this.SetValue(EvaluateJavascriptProperty, value); }
        }
    }
}
