using Widgets.Models;
using Microsoft.Maui.Controls.Compatibility;
using Grid = Microsoft.Maui.Controls.Compatibility.Grid;

namespace Widgets
{
    /// <summary>
    /// Popup menu widget
    /// </summary>
    public partial class NIQDarkPopup : ContentPage
    {
        #region Constants
        private const int PopupPadding = 20;
        private const int PopupTopPadding = 50;
        private const int InvalidIndex = -1;
        #endregion

        #region Private members
        private readonly Page rootPage;
        #endregion

        #region Events
        public delegate void ItemSelectedEventHandler(View item);
        public event ItemSelectedEventHandler ItemSelected;
        #endregion

        #region Properties
        /// <summary>
        /// Items list property
        /// </summary>
        public static readonly BindableProperty ItemsListProperty =
            BindableProperty.Create(nameof(ItemsList), typeof(List<PopupItem>), typeof(NIQDarkPopup));

        /// <summary>
        /// Selected property
        /// </summary>
        public static readonly BindableProperty SelectedProperty =
            BindableProperty.Create(nameof(Selected), typeof(int), typeof(NIQDarkPopup), InvalidIndex,
                propertyChanged: (sender, oldValue, newValue) =>
                {
                    if ((int)newValue == InvalidIndex)
                    {
                        return;
                    }
                    var popupMenu = sender as NIQDarkPopup;
                    int index = 0;
                    foreach (Microsoft.Maui.Controls.Grid item in popupMenu.popupListView.Children)
                    {
                        if (index == popupMenu.Selected)
                        {
                            var popupItem = item.Children.First(i => i is NIQDarkPopupItem) as NIQDarkPopupItem;
                            popupItem.IsSelected = true;
                            (sender as NIQDarkPopup).ItemSelected?.Invoke(item);
                        }
                        index++;
                    }
                });

        /// <summary>
        /// Selected property
        /// </summary>
        public static readonly BindableProperty SelectedListProperty =
            BindableProperty.Create(nameof(SelectedList), typeof(List<int>), typeof(NIQDarkPopup),
                propertyChanged: (sender, oldValue, newValue) =>
                {
                    var popupMenu = sender as NIQDarkPopup;
                    int index = 0;
                    foreach (Microsoft.Maui.Controls.Grid item in popupMenu.popupListView.Children)
                    {
                        if (popupMenu.SelectedList?.Contains(index) ?? false)
                        {
                            var popupItem = item.Children.First(i => i is NIQDarkPopupItem) as NIQDarkPopupItem;
                            popupItem.IsSelected = true;
                            popupItem.TextColor = NIQThemeController.Theme.Secondary;
                            popupItem.FontAttributes = FontAttributes.Bold; 
                        }
                        index++;
                    }
                });

        /// <summary>
        /// Close on item tapped property
        /// </summary>
        public static readonly BindableProperty AllowMultiSelectProperty =
            BindableProperty.Create(nameof(AllowMultiSelect), typeof(bool), typeof(NIQDarkPopup), false);

        /// <summary>
        /// Gets or sets items list
        /// </summary>
        public List<PopupItem> ItemsList
        {
            get => (List<PopupItem>)GetValue(ItemsListProperty);
            set => SetValue(ItemsListProperty, value);
        }

        /// <summary>
        /// Gets or sets selected item's index
        /// </summary>
        public int Selected
        {
            get => (int)GetValue(SelectedProperty);
            set => SetValue(SelectedProperty, value);
        }

        /// <summary>
        /// Gets or sets list of selected items
        /// </summary>
        public List<int> SelectedList
        {
            get => (List<int>)GetValue(SelectedListProperty);
            set => SetValue(SelectedListProperty, value);
        }

        /// <summary>
        /// Close on item tapped
        /// </summary>
        public bool AllowMultiSelect
        {
            get => (bool)GetValue(AllowMultiSelectProperty);
            set => SetValue(AllowMultiSelectProperty, value);
        }

        /// <summary>
        /// Gets or sets on item tapped command
        /// </summary>
        public Command<int> OnItemTapped { get; set; }

        /// <summary>
        /// Gets row height
        /// </summary>
        public int RowHeight => 50;

        /// <summary>
        /// Gets the max height
        /// </summary>
        public double MaxHeight => rootPage.Height * 0.75;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="NIQDarkPopup"/>
        /// </summary>
        /// <param name="rootPage">Root page</param>
        public NIQDarkPopup(Page rootPage)
        {
            InitializeComponent();

            this.rootPage = rootPage;

            BindingContext = this;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Show popup menu
        /// </summary>
        /// <param name="view">Root view</param>
        public void ShowPopup(View view)
        {
            Rect rect = GetViewRectangle(view);

            scrollView.WidthRequest = rootPage.Width * 0.4;
            scrollView.HeightRequest = CalculateHeight();

            RelativeLayout.SetXConstraint(listFrame, Constraint.Constant(CalculateLeftPosition(rect)));
            RelativeLayout.SetYConstraint(listFrame, Constraint.Constant(CalculateTopPosition(rect)));
            rootPage.Navigation.PushModalAsync(this, false);
        }

        /// <summary>
        /// Hides Popup
        /// </summary>
        public void HidePopup()
        {
            Close();
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Handles the state when the <see cref="Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await listFrame.ScaleTo(0, 1);
            IsVisible = true;
            listFrame.IsVisible = true;
            await listFrame.ScaleTo(1, 100);
        }

        /// <summary>
        /// Handles on back button pressed event
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            Close();
            return true;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Calculates popup left position depending on root view position
        /// </summary>
        /// <param name="rootViewRectangle"></param>
        /// <returns>Left position</returns>
        private int CalculateLeftPosition(Rect rootViewRectangle)
        {
            int popupWidth = (int)rootPage.Width / 2;
            popupListView.WidthRequest = popupWidth;

            int popupLeftPostition = (int)rootViewRectangle.Left;
            if (popupLeftPostition < PopupPadding)
            {
                popupLeftPostition = PopupPadding;
            }
            else if (popupLeftPostition + popupWidth > rootPage.Width - PopupPadding)
            {
                popupLeftPostition = (int)(rootPage.Width - popupWidth - PopupPadding);
            }

            return popupLeftPostition;
        }

        /// <summary>
        /// Calculates popup top position depending on root view position
        /// </summary>
        /// <param name="rootViewRectangle"></param>
        /// <returns>Top position</returns>
        private int CalculateTopPosition(Rect rootViewRectangle)
        {
            int popupHeight = (int)scrollView.HeightRequest;
            int popupTopPostition = (int)rootViewRectangle.Top - popupHeight - 5;
            if (popupTopPostition < PopupTopPadding)
            {
                popupTopPostition = (int)(rootViewRectangle.Top + rootViewRectangle.Height);
                if (popupTopPostition + popupHeight > rootPage.Height - PopupPadding)
                {
                    popupTopPostition = PopupTopPadding;
                }
            }

            return popupTopPostition;
        }

        /// <summary>
        /// Gets view global rectangle
        /// </summary>
        /// <param name="view">View</param>
        /// <returns>Global rectangle</returns>
        private Rect GetViewRectangle(View view)
        {
            var viewHelper = DependencyService.Get<IViewHelper>();
            return viewHelper.GetViewGlobalRectangle(view);
        }

        /// <summary>
        /// Handles popup item tapes
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private async void OnPopupItemTapped(object sender, EventArgs e)
        {
            Grid item = (Grid)sender;
            var recognizer = (TapGestureRecognizer)item.GestureRecognizers[0];
            var index = recognizer.CommandParameter;

            if (!AllowMultiSelect)
            {
                await Close();
            }
            OnItemTapped?.Execute(index);
        }

        /// <summary>
        /// Handles background tapes
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private async void OnBackgroundTapped(object sender, EventArgs e)
        {
            await Close();
        }

        /// <summary>
        /// Closes the popup
        /// </summary>
        /// <returns>Task</returns>
        private async Task Close()
        {
            listFrame.IsVisible = false;
            IsVisible = false;
            await rootPage?.Navigation.PopModalAsync(false);
        }

        /// <summary>
        /// Calculates menu height
        /// </summary>
        /// <returns>Menu height</returns>
        private int CalculateHeight()
        {
            int itemsCount = ItemsList?.Count ?? 0;
            int popupHeight = Math.Min(RowHeight * itemsCount, (int)MaxHeight);
            return popupHeight;
        }
        #endregion
    }

    /// <summary>
    /// Dark Popup Item
    /// </summary>
    public class NIQDarkPopupItem : Label
    {
        /// <summary>
        /// Indicates if current item is selected or not
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
