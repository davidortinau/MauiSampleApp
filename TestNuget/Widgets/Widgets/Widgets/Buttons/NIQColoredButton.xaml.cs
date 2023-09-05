namespace Widgets
{
    /// <summary>
    /// Colored button view
    /// </summary>
    public partial class NIQColoredButton : NIQButton
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQColoredButton"/> class
        /// </summary>
        public NIQColoredButton()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Background color property
        /// </summary>
        public static new readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(NIQColoredButton),
                defaultBindingMode: BindingMode.TwoWay, defaultValue:NIQThemeController.Theme.Primary);

        /// <summary>
        /// Gets or sets text color
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Pressed text color property
        /// </summary>
        public static readonly BindableProperty PressedTextColorProperty =
            BindableProperty.Create(nameof(PressedTextColor), typeof(Color), typeof(NIQColoredButton),
                defaultValue:NIQThemeController.Theme.OnPrimaryVariant);

        /// <summary>
        /// Pressed text color
        /// </summary>
        public Color PressedTextColor
        {
            get => (Color)GetValue(PressedTextColorProperty);
            set => SetValue(PressedTextColorProperty, value);
        }

        /// <summary>
        /// Text color property
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(NIQButton), defaultValue: NIQThemeController.Theme.OnPrimary);

        /// <summary>
        /// Gets or sets text color
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }
        #endregion
    }
}