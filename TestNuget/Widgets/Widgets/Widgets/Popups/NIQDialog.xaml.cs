using Widgets.Models;
using static Widgets.Enumerations;

namespace Widgets
{
    /// <summary>
    /// Alert view
    /// </summary>
    public partial class NIQDialog : ContentPage
    {
        /// <summary>
        /// Icon Tint Color property
        /// </summary>
        public static readonly BindableProperty IconTintColorProperty =
            BindableProperty.Create(nameof(IconTintColor), typeof(Color), typeof(NIQDialog));

        #region Properties
        /// <summary>
        /// Gets or sets tint color for icon
        /// </summary>
        public Color IconTintColor
        {
            get => (Color)GetValue(IconTintColorProperty);
            set => SetValue(IconTintColorProperty, value);
        }

        /// <summary>
        /// Gets or sets title text
        /// </summary>
        public new string Title
        {
            get => title.Text;
            set => title.Text = value;
        }

        /// <summary>
        /// Gets or sets title button color
        /// </summary>
        public Color TitleButtonTintColor { get; set; }

        /// <summary>
        /// Background color
        /// </summary>
        public new Color BackgroundColor => NIQThemeController.Theme.Background;
        
        /// <summary>
        /// On background color
        /// </summary>
        public Color TextColor => NIQThemeController.Theme.TitleColor;


        /// <summary>
        /// Retruns true if the dialog is shown otherwise false
        /// </summary>
        public bool IsShown { get; private set; } = false;

        /// <summary>
        /// Retruns true if dialog shouldn't be closed by Back button otherwise false
        /// This flag should be used got NIQNavigationPage navigation only
        /// </summary>
        public bool IsNotCloseByBack { get; set; } = false;
        #endregion

        #region Private fields
        private string logMessage;
        private string clickedButtonName;
        private string transactionId;
        private NIQDialogType dialogType;

        /// <summary>
        /// Minimum button width
        /// </summary>
        private const int MinButtonWidth = 100;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQDialog"/> class
        /// </summary>
        /// <param name="dialogTitle">Dialog title</param>
        /// <param name="actions">List of actions</param>
        /// <param name="contentView">List of views</param>
        /// <param name="dialogType">Dialog Type. Can be skipped to hide any icons</param>
        /// <param name="logMessage">Log Message</param>
        /// <param name="titleButtonInfo">Title button info</param>
        public NIQDialog(string dialogTitle, List<ButtonInfo> actions, List<View> contentView,
            NIQDialogType dialogType = NIQDialogType.None, string logMessage = null,
            DialogTitleButtonInfo titleButtonInfo = null)
        {
            InitializeComponent();

            title.Text = dialogTitle;
            SetDialogIcon(dialogType);

            if (titleButtonInfo != null)
            {
                titleButton.IsVisible = true;
                TitleButtonTintColor = titleButtonInfo.Color;
                titleButton.Source = titleButtonInfo.ImageSource;
                titleButton.Command = new Command(() =>
                {
                    titleButtonInfo.OnClicked?.Invoke();
                });
            }

            BindingContext = this;

            this.logMessage = logMessage;

            SetContentView(contentView);
            SetActions(actions);

            Disappearing += (s, args) => OnDismissed();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NIQDialog"/> class
        /// </summary>
        /// <param name="dialogTitle">Dialog title</param>
        /// <param name="actions">List of actions</param>
        /// <param name="view">Content views</param>
        /// <param name="dialogType">Dialog Type. Can be skipped to hide any icons</param>
        /// <param name="logMessage">Log Message</param>
        /// <param name="titleButtonInfo">Title button info</param>
        public NIQDialog(string dialogTitle, List<ButtonInfo> actions, View view,
            NIQDialogType dialogType = NIQDialogType.None, string logMessage = null,
            DialogTitleButtonInfo titleButtonInfo = null) : this(dialogTitle, actions,
                contentView: null, dialogType, logMessage, titleButtonInfo)
        {
            SetContentView(view);
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Process Dialog Type to show/hide icon
        /// </summary>
        /// <param name="dialogType">Dialog type</param>
        public Task SetDialogIcon(NIQDialogType dialogType)
        {
            return Device.InvokeOnMainThreadAsync(() =>
            {
                if (this.dialogType != dialogType)
                {
                    this.dialogType = dialogType;
                    icon.IsVisible = dialogType != NIQDialogType.None;
                    if (icon.IsVisible)
                    {
                        icon.Effects.Clear();
                        IconTintColor = GetIconColor(dialogType);
                        icon.Source = GetDialogIcon(dialogType);
                    };
                }
            });
        }

        /// <summary>
        /// Shows the alert
        /// </summary>
        /// <param name="page">Root page</param>
        public void Show(Page page)
        {
            if (page != null)
            {
                transactionId = Guid.NewGuid().ToString();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await page.Navigation.PushModalAsync(this, animated: false);
                });

                IsShown = true;
            }
        }

        /// <summary>
        /// Shows the dialog if app is unlocked
        /// </summary>
        /// <param name="getPage">Get root page function</param>
        public async Task ShowWhenUnlocked(Func<Page> getPage)
        {
            Show(getPage());
        }

        /// <summary>
        /// Dismisses the alert
        /// </summary>
        /// <param name="page">Root page</param>
        /// <param name="finishAction">Finish action</param>
        public void Dismiss(Page page, Action finishAction = null)
        {
            DismissAsync(page, finishAction);
        }

        /// <summary>
        /// Dismisses the alert async
        /// </summary>
        /// <param name="page">Root page</param>
        /// <param name="finishAction">Finish action</param>
        public async Task DismissAsync(Page page, Action finishAction = null)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                if (page != null && page.Navigation.ModalStack.Any())
                {
                    await page.Navigation.RemoveModalPage(this);
                }
                finishAction?.Invoke();
            });
            IsShown = false;
        }

        /// <summary>
        /// Sets actions enabled state
        /// </summary>
        /// <param name="isEnabled">Enabled state</param>
        public Task SetActionsEnabledState(bool isEnabled)
        {
            return Device.InvokeOnMainThreadAsync(() =>
            {
                actionsLayout.IsEnabled = isEnabled;
                foreach (var child in actionsLayout.Children)
                {
                    switch (child)
                    {
                        case NIQButton buttonView:
                            buttonView.IsEnabled = isEnabled;
                            break;
                        /*default:
                            child.IsEnabled = isEnabled;
                            break;*/
                    }
                }
            });
        }

        /// <summary>
        /// Sets content view
        /// </summary>
        /// <param name="contentView">List of views</param>
        public virtual void SetContentView(List<View> contentView)
        {
            if (contentView != null && contentLayout?.Children != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    contentLayout.Children.Clear();
                    contentView.ForEach(item => contentLayout.Children.Add(item));
                });
            }
        }

        /// <summary>
        /// Sets content view
        /// </summary>
        /// <param name="contentView">View</param>
        public virtual void SetContentView(View contentView)
        {
            if (contentView != null && contentLayout?.Children != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    contentLayout.Children.Clear();
                    contentLayout.Children.Add(contentView);
                });
            }
        }

        /// <summary>
        /// Sets actions
        /// </summary>
        /// <param name="actions">List of actions</param>
        public virtual async Task SetActions(List<ButtonInfo> actions)
        {
            if (actionsLayout != null && actionsLayout.Children != null)
            {
                await Device.InvokeOnMainThreadAsync(() =>
                {
                    actionsLayout.Children.Clear();
                    actions?.OrderBy(action => action.ButtonType)
                        .ToList()
                        .ForEach(action =>
                        {
                            NIQButton button = CreateButtonView(action);
                            actionsLayout.Children.Add(button);
                        });
                });
            }
        }

        /// <summary>
        /// Sets action
        /// </summary>
        /// <param name="action">Button info</param>
        public Task SetAction(ButtonInfo action)
        {
            return SetActions(new List<ButtonInfo>()
            {
                action
            });
        }

        /// <summary>
        /// Adds action
        /// </summary>
        /// <param name="action">Button info</param>
        public void AddAction(ButtonInfo action)
        {
            if (actionsLayout != null && actionsLayout.Children != null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    NIQButton button = CreateButtonView(action);
                    actionsLayout.Children.Add(button);
                });
            }
        }

        /// <summary>
        /// Gets existing button view by title
        /// </summary>
        /// <param name="title">Button title</param>
        /// <returns>Button view</returns>
        public NIQButton GetExistingButtonView(string title)
        {
            string convertedTitle = ConvertButtonTitle(title);
            return actionsLayout.Children.FirstOrDefault(view => (view as NIQButton)?.Text == convertedTitle) as NIQButton;
        }

        /// <summary>
        /// Toggles title button visibility
        /// </summary>
        /// <param name="isVisible">Flag, which indicates visibility state</param>
        public Task ToggleTitleButtonVisibility(bool isVisible)
        {
            return Device.InvokeOnMainThreadAsync(() =>
            {
                titleButton.IsVisible = isVisible;
            });
        }
        #endregion

        #region Protected methods
        /// <summary>
        /// Handles back button press
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        /// <summary>
        /// Called when the alert is closed
        /// </summary>
        protected void OnDismissed()
        {
            var step = !string.IsNullOrEmpty(clickedButtonName) ? 1 : 2;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Gets button view depending on provided info
        /// </summary>
        /// <param name="action">Action info</param>
        /// <returns>Button view</returns>
        private NIQButton CreateButtonView(ButtonInfo action)
        {
            NIQButton button;
            if (NIQThemeController.Theme == Theme.Light)
            {
                button = new NIQBorderlessButton();
                button.Text = ConvertButtonTitle(action.Title);
            }
            else
            {
                switch (action.ButtonType)
                {
                    case ButtonType.Positive:
                        button = new NIQColoredButton();
                        break;
                    case ButtonType.Negative:
                        button = new NIQBorderlessButton();
                        break;
                    default:
                        button = new NIQOutlineButton();
                        break;
                }
                button.Text = ConvertButtonTitle(action.Title);
            }
            button.Command = new Command(() =>
            {
                clickedButtonName = action.Title;
                action.OnClicked?.Invoke(button);
            });
            button.MinimumWidthRequest = MinButtonWidth;
            button.IsDialogButton = true;
            button.IsEnabled = action.IsEnabled;
            return button;
        }

        /// <summary>
        /// Converts button title
        /// </summary>
        /// <param name="title">Title</param>
        /// <returns>Converted title</returns>
        private string ConvertButtonTitle(string title)
        {
            return NIQThemeController.Theme == Theme.Light
                ? title.ToUpper()
                : title;
        }

        /// <summary>
        /// Returns icon for dialog depends on dialog type
        /// </summary>
        /// <param name="dialogType">NIQ Dialog Type</param>
        /// <returns></returns>
        private ImageSource GetDialogIcon(NIQDialogType dialogType)
        {
            string fileName = string.Empty;
            switch (dialogType)
            {
                case NIQDialogType.PositiveConfirmation:
                    fileName = "positive_niq";
                    break;
                case NIQDialogType.Error:
                case NIQDialogType.Warning:
                    fileName = "error_niq";
                    break;
                case NIQDialogType.Information:
                    fileName = "info_niq";
                    break;
                case NIQDialogType.Question:
                    fileName = "question_niq";
                    break;
            }
            return ImageSource.FromResource($"Widgets.Images.{fileName}.png");
        }

        /// <summary>
        /// Returns color for the dialog icon
        /// </summary>
        /// <param name="dialogType">NIQ Dialog Type</param>
        /// <returns></returns>
        private Color GetIconColor(NIQDialogType dialogType)
        {
            Color color = Utilities.DefaultColor;
            switch (dialogType)
            {
                case NIQDialogType.PositiveConfirmation:
                    color = NIQThemeController.Theme.Positive;
                    break;
                case NIQDialogType.Error:
                    color = NIQThemeController.Theme.Error;
                    break;
                case NIQDialogType.Warning:
                    color = NIQThemeController.Theme.Warning;
                    break;
                case NIQDialogType.Information:
                    color = NIQThemeController.Theme.DialogInfoColor;
                    break;
                case NIQDialogType.Question:
                    color = NIQThemeController.Theme.OnBackground;
                    break;
            }
            return color;
        }

        /// <summary>
        /// Blocks current thread while app is locked
        /// </summary>
        private static async Task WaitForUnlock()
        {
            var waitHandle = new AutoResetEvent(false);
            void handler()
            {
                waitHandle.Set();
            }
            await Task.Run(waitHandle.WaitOne);
        }
        #endregion
    }
}
