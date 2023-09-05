namespace Widgets
{
    /// <summary>
    /// NIQ Image Button
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NIQImageButton : ImageButton
    {
        private const int DefaultCornerRadius = -1;
        private const int CornerRadiusPadding = 5;

        /// <summary>
        /// PressedColor property
        /// </summary>
        public static readonly BindableProperty PressedColorProperty =
            BindableProperty.Create(nameof(PressedColor), typeof(Color), typeof(NIQImageButton), NIQThemeController.Theme.Surface);

        /// <summary>
        /// Gets or sets pressed color
        /// </summary>
        public Color PressedColor
        {
            get { return (Color)GetValue(PressedColorProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }


        /// <summary>
        /// Gets the background color for pressed state
        /// </summary>
        public Color BackgroundPressedColor => Command != null ?
            PressedColor : Colors.Transparent;

        /// <summary>
        /// Initializes new instance of <see cref="NIQImageButton"/>
        /// </summary>
        public NIQImageButton()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method is called when the size of the element is set during a layout cycle.
        /// This method is called directly before the Xamarin.Forms.VisualElement.SizeChanged
        /// event is emitted. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="width">The new width of the element.</param>
        /// <param name="height">The new height of the element.</param>
        protected override void OnSizeAllocated(double width, double height)
        {
            if (CornerRadius == DefaultCornerRadius)
            {
                CornerRadius = (int)height / 2 + CornerRadiusPadding;
            }
            base.OnSizeAllocated(width, height);
        }
    }
}