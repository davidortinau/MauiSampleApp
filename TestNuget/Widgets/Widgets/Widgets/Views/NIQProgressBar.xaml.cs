namespace Widgets
{
    /// <summary>
    /// Progress bar view
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NIQProgressBar : ContentView
    {
        /// <summary>
        /// Message property
        /// </summary>
        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(NIQProgressBar),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var current = bindable as NIQProgressBar;
                    current.messageLabel.Text = (string)newValue;
                });

        /// <summary>
        /// Progress property
        /// </summary>
        public static readonly BindableProperty ProgressProperty =
            BindableProperty.Create(nameof(Progress), typeof(int), typeof(NIQProgressBar),
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var current = bindable as NIQProgressBar;
                    current.progressBar.ProgressTo((int)newValue / (float)current.MaxProgress, 250, Easing.Linear);
                });

        /// <summary>
        /// Maximum progress property
        /// </summary>
        public static readonly BindableProperty MaxProgressProperty =
            BindableProperty.Create(nameof(MaxProgress), typeof(int), typeof(NIQProgressBar));

        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// Gets or sets progress
        /// </summary>
        public int Progress
        {
            get => (int)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        /// <summary>
        /// Gets or sets maximum progress
        /// </summary>
        public int MaxProgress
        {
            get => (int)GetValue(MaxProgressProperty);
            set => SetValue(MaxProgressProperty, value);
        }

        /// <summary>
        /// Initialize new instance of <see cref="NIQProgressBar"/>
        /// </summary>
        /// <param name="maxProgress">Maximum progress</param>
        public NIQProgressBar(int maxProgress = 1)
        {
            MaxProgress = maxProgress;
            InitializeComponent();
        }
    }
}