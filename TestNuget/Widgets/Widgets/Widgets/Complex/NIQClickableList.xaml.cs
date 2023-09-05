using Widgets.Models;

namespace Widgets
{
    /// <summary>
    /// Clickable list
    /// </summary>
    public partial class NIQClickableList : ScrollView
    {
        #region Private members
        private readonly string title;
        #endregion

        #region Properties
        /// <summary>
        /// Items list property
        /// </summary>
        public static readonly BindableProperty ItemsListProperty =
            BindableProperty.Create(nameof(ItemsList), typeof(List<ListItem>), typeof(NIQClickableList));

        /// <summary>
        /// Gets or sets items list
        /// </summary>
        public List<ListItem> ItemsList
        {
            get => (List<ListItem>)GetValue(ItemsListProperty);
            set => SetValue(ItemsListProperty, value);
        }

        /// <summary>
        /// Gets or sets on item tapped command
        /// </summary>
        public Command<int> OnItemTapped { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes an instance of <see cref="NIQClickableList"/>
        /// </summary>
        /// <param name="title">Title</param>
        public NIQClickableList(string title)
        {
            InitializeComponent();

            this.title = title;

            BindingContext = this;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Handles on item tapped event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event arguments</param>
        private void OnListItemTapped(object sender, EventArgs e)
        {
            NIQPopupItem popupItem = (NIQPopupItem)sender;
            var recognizer = (TapGestureRecognizer)popupItem.GestureRecognizers[0];
            var index = (int)recognizer.CommandParameter;

            OnItemTapped?.Execute(index);
        }
        #endregion
    }
}
