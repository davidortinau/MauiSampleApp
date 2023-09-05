namespace Widgets
{
    public partial class NIQOutlineButton : NIQButton
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQOutlineButton"/> class
        /// </summary>
        public NIQOutlineButton()
        {
            InitializeComponent();
            PressedTextColor = NIQThemeController.Theme.OnPrimaryVariant;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Border color property
        /// </summary>
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(NIQOutlineButton),
                defaultBindingMode: BindingMode.TwoWay);

        /// <summary>
        /// Gets or sets border color
        /// </summary>
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        #endregion
    }
}
