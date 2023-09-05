using Utilities;
using Widgets;
using Widgets.Models;

namespace Chrono
{
    /// <summary>
    /// Chrono Calendar Page
    /// </summary>
    public partial class CalendarPage : ChronoBasePage, INavigationPopInterceptor
    {
        #region Constants
        private const string TAG = nameof(CalendarPage);
        #endregion

        #region Private fields
        private NIQDarkPopup ttModePopup;
        private NIQPopupMenu syncOptionsPopup;
        private CalendarTitleView titleView;
        private bool isPopRequest = false;
        private ChronoListView selectedListView = null;
        #endregion

        #region Public properties
        public CalendarViewModel viewModel => BindingContext as CalendarViewModel;

        public bool IsPopRequest
        {
            get
            {
                return isPopRequest;
            }
            set
            {
                isPopRequest = value;
            }
        }
        #endregion

        /// <summary>
        /// Initializes the instance of <see cref="CalendarPage.cs"/>
        /// </summary>
        /// <param name="selectedDate">Selected Date</param>
        public CalendarPage(DateTime? selectedDate = null)
        {
            InitializeComponent();
            BindingContext = new CalendarViewModel(Navigation, selectedDate);
            viewModel.ScrollToPosition = (position) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ActivitiesCarouselView.ScrollTo(position, position: ScrollToPosition.Center);
                });
            };
            InitTitleView();
        }

        /// <summary>
        /// Raises on hardware back pressed event
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            viewModel.ClosePage(false);
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Raises back pressed event in case if shell not presented
        /// </summary>
        /// <returns></returns>
        public Task<bool> RequestPop()
        {
            viewModel.ClosePage(false);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Updates day data
        /// </summary>
        /// <param name="datesToUpdate">Dates to update</param>
        public void UpdateDayData(List<DateTime> datesToUpdate)
        {
            viewModel.UpdateDayData(datesToUpdate);
        }

        /// <summary>
        /// Go to selected date
        /// </summary>
        /// <param name="date">Date to update</param>
        public void GoToSelectedDay(DateTime selectedDate)
        {
            viewModel.DateSelected(selectedDate);
        }

        #region Private methods
        /// <summary>
        /// Initializes Title View
        /// </summary>
        private void InitTitleView()
        {
            titleView = new CalendarTitleView();
            Shell.SetTitleView(this, titleView);
            NavigationPage.SetTitleView(this, titleView);
        }

        /// <summary>
        /// Initializes sync popup
        /// </summary>
        private void InitSyncPopupMenu()
        {
            var itemsList = new List<PopupItem>()
            {
                new PopupItem(){Index = 0, Title = MCOEStringResources.Fetch,
                    Enabled = true},
                new PopupItem(){Index = 1, Title = MCOEStringResources.Submit, 
                    Enabled = true}
            };
            syncOptionsPopup = new NIQPopupMenu(this)
            {
                ItemsList = itemsList,
                OnItemTapped = new Command<int>(async (index) =>
                {
                    var selectedItemValue = itemsList[index].Title;
                    await OptionsMenuItemClickedAsync(selectedItemValue.ToString());
                })
            };
        }

        /// <summary>
        /// Raises time interval clicked event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void ChronoListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            long currentTouchTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var interval = e.SelectedItem as TimeInterval;
            if (interval.IntervalMode == IntervalMode.Editable ||
                interval.IntervalMode == IntervalMode.Empty ||
                interval.IntervalMode == IntervalMode.EmptyOptional)
            {
                var actItem = viewModel.ActivityItems.Where(act => act.Date == interval.Date);
                var selectedTimeIntervals = actItem.FirstOrDefault().TimeIntervals.Where(interval => interval.IsSelected);
                if (!selectedTimeIntervals.Any() || (selectedTimeIntervals.Any() && !selectedTimeIntervals.Contains(interval)))
                {
                    //viewModel.NavigateToActivity(interval);
                }
                else if (selectedTimeIntervals.Any())
                {
                    var startTime = selectedTimeIntervals.First().StartTime;
                    var endTime = selectedTimeIntervals.Last().EndTime;
                    var selectedInterval = new TimeInterval(interval.Date, startTime, endTime - startTime, IntervalMode.Empty);
                    //viewModel.NavigateToActivity(selectedInterval);
                }
                selectedListView?.ResetSelection();
            }
        }

        /// <summary>
        /// Raises time interval long clicked event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void ChronoListView_LongClick(object sender, SelectedItemChangedEventArgs e)
        {
            if (selectedListView != null && !selectedListView.Items[0].Date.Date.Equals((e.SelectedItem as TimeInterval).Date.Date))
            {
                selectedListView?.ResetSelection();
            }
            if ((e.SelectedItem as TimeInterval).IsSelected)
            {
                selectedListView = sender as ChronoListView;
            }
            else
            {
                selectedListView = null;
            }
        }


        /// <summary>
        /// Performs Fetch and Submit operation
        /// </summary>
        /// <param name="OptionsMenuTitle"></param>
        private async Task OptionsMenuItemClickedAsync(string OptionsMenuTitle)
        {
        }
        #endregion
    }
}