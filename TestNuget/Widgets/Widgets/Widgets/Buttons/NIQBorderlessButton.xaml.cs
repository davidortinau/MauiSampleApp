namespace Widgets
{
    /// <summary>
    /// Borderless button view
    /// </summary>
    public partial class NIQBorderlessButton : NIQButton
    {
        #region Properties
        /// <summary>
        /// Gets flag, which indicates, if underlined text is allowed
        /// </summary>
        public virtual bool IsUnderlinedTextAllowed => true;
        /// <summary>
        /// Gets flag, which indicates, if upper case text is allowed
        /// </summary>
        public virtual bool IsUpperCaseAllowed => true;
        /// <summary>
        /// Gets or sets height request for the button
        /// </summary>
        public double HeightRequest
        {
            get { return button.HeightRequest; }
            set { button.HeightRequest = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQBorderlessButton"/> class
        /// </summary>
        public NIQBorderlessButton()
        {
            InitializeComponent();
            PressedTextColor = NIQThemeController.Theme.OnPrimaryVariant;
        }
        #endregion
    }
}
