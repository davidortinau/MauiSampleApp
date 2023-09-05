using Widgets;
using Widgets.Models;
using System.ComponentModel;
using System.Windows.Input;
using WidgetsUtils = Widgets.Utilities;

namespace Chrono
{
    /// <summary>
    /// Calendar View Model
    /// </summary>
    public class CalendarViewModel : INotifyPropertyChanged
    {
        #region Constants
        private const string TAG = nameof(CalendarViewModel);
        private const string WeekStartEndDateFormat = "{0}-{1}";
        private const string ShortWeekDateFormat = "MM/dd";
        private const string MonthNameFormat = "MMMM";
        #endregion

        #region Private fields

        private string modeTitle = string.Empty;
        private string titleDateValue = string.Empty;
        private List<ChronoDayInfo> activityItems = new List<ChronoDayInfo>();
        private INavigation navigation;
        private bool isLoading = false;
        private int currentPosition = 0;
        private DateTime selectedDate = DateTime.Today;
        private ChronoDayInfo currentDayItem;
        private int manualSetPosition = 0;
        private ImageSource expandImage;
        #endregion

        #region Public fields
        public event PropertyChangedEventHandler PropertyChanged;

        public Action<TimeInterval> SelectedTimeInterval;

        public Action<DateTime, TimeInterval> NavigateToNewActivityPage;

        public List<PopupItem> PopupItems { get; private set; }

        public Func<DateTime, DateTime, Task<List<ChronoDayInfo>>> GetChronoDataAction { get; internal set; }

        public Action<int> ScrollToPosition { get; set; }

        public Command OnBackPressed { get; set; }

        public ICommand SyncButtonClicked { get; set; }

        public ICommand CalendarButtonClicked { get; set; }

        public ICommand AddActivityButtonClicked { get; set; }
        #endregion

        #region Properties
        public ImageSource ExpandImage
        {
            get
            {
                return expandImage;
            }
            set
            {
                expandImage = value;
                OnPropertyChanged(nameof(ExpandImage));
            }
        }
        public string ModeTitle
        {
            get
            {
                return modeTitle;
            }
            set
            {
                if (!modeTitle.Equals(value))
                {
                    modeTitle = value;
                }
                OnPropertyChanged(nameof(ModeTitle));
            }
        }

        public ChronoDayInfo CurrentDayItem
        {
            get
            {
                return currentDayItem;
            }
            set
            {
                currentDayItem = value;
                OnPropertyChanged(nameof(CurrentDayItem));
            }
        }

        public int CurrentPosition
        {
            get
            {
                return currentPosition;
            }
            set
            {
                currentPosition = value;
                if (currentPosition >= 0 && currentPosition < ActivityItems.Count)
                {
                    selectedDate = ActivityItems[currentPosition].Date;
                }
                OnPropertyChanged(nameof(CurrentPosition));
            }
        }

        public int ManualSetPosition
        {
            get
            {
                return manualSetPosition;
            }
            set
            {
                manualSetPosition = value;
                OnPropertyChanged(nameof(ManualSetPosition));

                CurrentPosition = value;
            }
        }

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public List<ChronoDayInfo> ActivityItemsSource
        {
            get { return activityItems; }
            set
            {
                var oldIndex = activityItems.FindIndex(it => it.Date.Date == selectedDate.Date);
                activityItems = value;

                if (ActivityItems == null)
                {
                    ActivityItems = new ObservableCollectionExtention<ChronoDayInfo>(activityItems);
                }
                else if (ActivityItems.Count != activityItems.Count)
                {
                    var newIndex = activityItems.FindIndex(it => it.Date.Date == selectedDate.Date);
                    var itemsBeforeCurrent = newIndex == 0 ? new List<ChronoDayInfo>() : activityItems.GetRange(0, newIndex);
                    var itemsAfterCurrent = newIndex == activityItems.Count - 1 ? new List<ChronoDayInfo>() : activityItems.GetRange(newIndex + 1, activityItems.Count - newIndex - 1);

                    if (ActivityItems.Count < activityItems.Count)
                    {
                        if (ActivityItems.Count != 1)
                        {
                            ActivityItems.RemoveItems(0, oldIndex - 1);
                            ActivityItems.RemoveItems(1, ActivityItems.Count - 1);
                        }

                        ActivityItems.InsertItems(itemsBeforeCurrent, 0);
                        ActivityItems.AddItems(itemsAfterCurrent);

                        ActivityItems[newIndex].Date = activityItems[newIndex].Date;
                        ActivityItems[newIndex].UpdateTimeIntervals(activityItems[newIndex].TimeIntervals);
                    }
                    else if (ActivityItems.Count > activityItems.Count)
                    {
                        if ((newIndex == 0 && itemsBeforeCurrent.Count == 0) || (newIndex == activityItems.Count - 1 && itemsAfterCurrent.Count == 0))
                        {
                            ActivityItems.RemoveItems(activityItems.Count-1, ActivityItems.Count - activityItems.Count);
                            for (int i = 0; i < activityItems.Count; i++)
                            {
                                ActivityItems[i].Date = activityItems[i].Date;
                                ActivityItems[i].UpdateTimeIntervals(activityItems[i].TimeIntervals);
                            }
                        }
                        else
                        {
                            var itemsToReplaceBefore = ActivityItems.Take(oldIndex).ToList();
                            var itemsToReplaceAfter = ActivityItems.TakeLast(ActivityItems.Count - oldIndex - 1).ToList();
                            ActivityItems.Replace(itemsToReplaceBefore, itemsBeforeCurrent);
                            ActivityItems.Replace(itemsToReplaceAfter, itemsAfterCurrent);
                        }
                    }
                }
                else
                {
                    //Changing dates within one view mode
                    for (int i = 0; i < activityItems.Count; i++)
                    {
                        ActivityItems[i].Date = activityItems[i].Date;
                        ActivityItems[i].UpdateTimeIntervals(activityItems[i].TimeIntervals);
                    }
                }
                OnPropertyChanged(nameof(ActivityItems));
            }
        }

        public ObservableCollectionExtention<ChronoDayInfo> ActivityItems { get; private set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of <see cref=""="CalendarViewModel.cs"/>
        /// </summary>
        /// <param name="navigation">Navigation</param>
        /// <param name="initialDate">Initial date</param>
        public CalendarViewModel(INavigation navigation, DateTime? initialDate)
        {
            this.navigation = navigation;
            selectedDate = initialDate.HasValue ? initialDate.Value : selectedDate;
            SelectedTimeInterval = OnTimeIntervalSelected;
            /*if (NIQThemeController.Theme == Theme.NIQRebrand)
            {
                ExpandImage = ImageSource.FromResource("Widgets.Images.expand_white.png", typeof(ImageResourceExtension));
            }
            else
            {
                ExpandImage = ImageSource.FromResource("Widgets.Images.expand_green.png", typeof(ImageResourceExtension));
            }*/
            OnBackPressed = new Command(() =>
            {
                ClosePage(true);
            });
        }

        #region Private methods
        /// <summary>
        /// Do some actions before page will be closed
        /// </summary>
        /// <param name="isPopRequired">Indicates wherer page should be popped from the current stack</param>
        internal void ClosePage(bool isPopRequired)
        {
            if (isPopRequired)
            {
                _ = WidgetsUtils.GetLastPage().Navigation.PopAsync();
            }
        }

        /// <summary>
        /// Raises On Time Interval Selected Event
        /// </summary>
        /// <param name="timeInterval"></param>
        private void OnTimeIntervalSelected(TimeInterval timeInterval)
        {
        }

        /// <summary>
        /// Updates Time Tracker Data based on selected Time Tracker View Mode
        /// </summary>
        /// <returns></returns>
        private async Task UpdateTimeTrackerData(DateTime? newDate)
        {
            selectedDate = newDate.HasValue ? newDate.Value : selectedDate;
            IsLoading = true;
            var (items, pos) = await ChronoManager.Instance.ReloadTimeTrackerData(selectedDate);
            ActivityItemsSource = items;
            ManualSetPosition = pos;
            CurrentDayItem = items[pos];
            IsLoading = false;
        }

        /// <summary>
        /// Updates Day Data
        /// </summary>
        /// <param name="datesToUpdate">Dates to update</param>
        internal async void UpdateDayData(List<DateTime> datesToUpdate)
        {
            var updatedData = await GetChronoDataAction(datesToUpdate.First(), datesToUpdate.Last());
            var itemsToUpdate = ActivityItems.Where(item => item.Date.Date >= datesToUpdate.First() && item.Date.Date <= datesToUpdate.Last());

            if (itemsToUpdate != null && itemsToUpdate.Any())
            {
                foreach (var item in itemsToUpdate)
                {
                    var index = ActivityItems.IndexOf(item);
                    var updatedTimeIntervals = updatedData.FirstOrDefault(data => data.Date.Date == item.Date.Date).TimeIntervals;
                    ActivityItems[index].UpdateTimeIntervals(updatedTimeIntervals);
                }
            }
        }

        /// <summary>
        /// Raises Date Selected Event
        /// </summary>
        /// <param name="newDate">Selected Date</param>
        internal void DateSelected(DateTime newDate)
        {
            if (this.selectedDate != newDate.Date)
            {
                var loadedDay = ActivityItemsSource.FirstOrDefault(item => item.Date.Date == newDate.Date);
                if (loadedDay != null)
                {
                    var index = ActivityItemsSource.IndexOf(loadedDay);
                    ScrollToPosition(index);
                }
                else
                {
                    UpdateTimeTrackerData(newDate);
                }
            }
        }

        /// <summary>
        /// Raises on property changed event
        /// </summary>
        /// <param name="propName">Property name</param>
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        #endregion
    }
}

