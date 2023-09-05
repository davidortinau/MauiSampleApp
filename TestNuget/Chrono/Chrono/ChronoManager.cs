using Utilities;
using WidgetsUtils = Widgets.Utilities;
using Chrono.Constants;
using Chrono.Utilities;
using ChronoDatabase.Models;
using Microsoft.Maui.Embedding;

namespace Chrono
{
    public class ChronoManager
    {
        #region Constants
        private const string TAG = nameof(ChronoManager);

        private static readonly TimeSpan MaxTime = TimeSpan.FromHours(24);
        private static readonly TimeSpan MinTime = TimeSpan.FromHours(0);
        #endregion

        #region Private members
        private INavigation navigation;

        private static ChronoManager instance;
        private DateTime FirstLaunchDate;

        private bool isNativeApplication = false;

        protected delegate bool TimeGapHandler(List<ChronoActivityInfo> dayAct, TimeSpan fromTime, TimeSpan toTime);

        internal TimeSpan TimeStep = TimeSpan.FromHours(0.5);
        internal TimeSpan DayStartTime;
        internal TimeSpan DayEndTime;
        internal int DayStartDay;
        internal int DayEndDay;
        internal int MinActivityDurationSec = 300;
        internal List<int> LongActivityTypes = new List<int>();

        internal CalendarPage calendarPage;
        internal bool isChronoOnTop = false;

        private IChronoNativeHelper nativeHelper;

        private bool isTimerSubscribed = false;

        private bool IsInitialFetchDone = false;
        private bool IsInitialAutomaticActivityUpdated = false;
        private bool IsAppInBackground = false;
        #endregion

        #region Enums
        /// <summary>
        /// New Activity Page Activity Type:
        /// manual, manual after long inactivity, atypical after long inactivity
        /// </summary>
        public enum ChronoNewActivityActionType : int
        {
            Manual,
            LongManual,
            LongAtypical,
            Break
        }

        /// <summary>
        /// Fetch process Mode.
        /// </summary>
        public enum ChronoFetchMode : int
        {
            Silence = 0,
            WithDialog
        }

        /// <summary>
        /// Get and set the flag that is Chrono on top
        /// </summary>
        public bool IsChronoOnTop
        {
            get
            {
                return IsChronoScreenOnTop();
            }
            set
            {
                isChronoOnTop = value;
            }
        }
        #endregion

        #region Public properties      
        /// <summary>
        /// Chrono Mananger Instance
        /// </summary>
        public static ChronoManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ChronoManager();
                }
                return instance;
            }
        }

        /// <summary>
        /// Mock environment cloud detais path for Chrono
        /// </summary>
        public string MockChronoCloudDetailsFilePath;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChronoManager"/> class
        /// </summary>
        private ChronoManager()
        {
            nativeHelper = DependencyService.Get<IChronoNativeHelper>();

            if (Application.Current == null)
            {
                Application.Current = new ChronoApp(true);
            }
            // Init working days by default
            DayStartTime = ConvertTime(ChronoConstants.ChronoWorkTimeStartDefault).Value;
            DayEndTime = ConvertTime(ChronoConstants.ChronoWorkTimeEndDefault).Value;
            DayStartDay = ConvertDay(ChronoConstants.ChronoWorkDayStartDefault);
            DayEndDay = ConvertDay(ChronoConstants.ChronoWorkDayEndDefault);

            IntilaizeDefaultFirstLaunchDate();
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Sets first launch date
        /// </summary>
        /// <param name="launchDate">First launch Date</param>
        public void SetFirstLaunchDate(DateTime launchDate)
        {
            FirstLaunchDate = launchDate;
        }

        /// <summary>
        /// Starts Chrono calendar page
        /// </summary>
        /// <param name="navigation">Navigation</param>
        public void StartChrono(INavigation navigation)
        {
            ShowChronoPage(navigation);
        }

        /// <summary>
        /// Starts Chrono calendar page from Native Android activity
        /// </summary>
        /// <returns></returns>
        public void StartChronoFromNative()
        {
            nativeHelper.OpenChronoCalendarPage();
        }

        /// <summary>
        /// Shows long inactivity acitivty dialog
        /// </summary>
        /// <param name="auditInfo">Audit Info for atypical activity</param>
        /// <param name="inactivityStartTime">Inactivity start time</param>
        /// <param name="selectedAction">Selected action callback</param>
        public void ShowLongInactivyDialog(ChronoAuditInfo auditInfo, DateTime inactivityStartTime, Action<ChronoNewActivityActionType> selectedAction)
        {
            var longInactivityDialog = ChronoDialogHelper.ShowLongInactivyDialog(auditInfo, inactivityStartTime, selectedAction);
            var lastPage = WidgetsUtils.GetLastPage();
            longInactivityDialog.Show(lastPage);
        }

        /// <summary>
        /// Shows Long Inacitivty dialog in native android activity
        /// </summary>
        /// <param name="auditInfo">Audit Info for atypical activity</param>
        /// <param name="inactivityStartTime">Inactivity start time</param>
        /// <param name="selectedAction">Selected action callback</param>
        public void ShowLongInactivityDialogNative(ChronoAuditInfo auditInfo, DateTime inactivityStartTime, Action<ChronoNewActivityActionType> selectedAction)
        {
            nativeHelper.ShowChronoLongInactivityDialog(auditInfo, inactivityStartTime, selectedAction);
        }

        /// <summary>
        /// Set Application Type
        /// </summary>
        /// <param name="isNative">true id Xamarin Native Application, false if Xamarin Forms.</param>
        public void SetNativeApplicationType(bool isNative)
        {
            isNativeApplication = isNative;
        }

        /// <summary>
        /// Provides information Chrono screen on top or not
        /// </summary>
        /// <returns>true - if any Chrono screen or Dialog on top, false - in another cases</returns>
        public bool IsChronoScreenOnTop()
        {
            var lastPage = WidgetsUtils.GetLastPage();
            var navStackContainsChronoPage = false;
            if (lastPage != null)
            {
                if (lastPage is ChronoBasePage)
                {
                    navStackContainsChronoPage = (lastPage as ChronoBasePage).IsActive || lastPage?.Navigation?.ModalStack?.Count != 0;
                }
                else if (lastPage is NIQChronoDialog)
                {
                    navStackContainsChronoPage = (lastPage as NIQChronoDialog).IsActive;
                }
            }
            return isChronoOnTop || navStackContainsChronoPage;
        }

        /// <summary>
        /// Inform Applications background state changed based infromation from host application
        /// </summary>
        /// <param name="isInBackground">false - if application go to foreground, true - application go to background</param>
        public void NotifyAppBackgroundStateChanged(bool isInBackground)
        {
            IsAppInBackground = isInBackground;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Get should we show screen as Native screen or as Form screen
        /// </summary>
        /// <returns>true - if new screen should be shown as native, false - as forms</returns>
        internal bool IsShowAsNative()
        {
            bool hasChronoFromsPage = false;

            var lastPage = WidgetsUtils.GetLastPage();
            if (lastPage != null)
            {
                var activeNavigation = Application.Current?.MainPage?.Navigation;
                if (activeNavigation != null &&
                    activeNavigation.NavigationStack != null
                    && activeNavigation.NavigationStack.Any(page => page != null && (page is ChronoBasePage)))
                {
                    hasChronoFromsPage = true;
                }
            }
            return isNativeApplication && (lastPage == null || !hasChronoFromsPage);
        }

        /// <summary>
        /// Set the start and end time and start and end day of week
        /// of the auditor's working day and start and end of week
        /// </summary>
        /// <param name="workingDayStart">Start time of auditor's working day</param>
        /// <param name="workingDayEnd">End time of auditor's working day</param>
        /// <param name="startWorkDay">Start day in week of auditor's working day</param>
        /// <param name="endWorkDay">End day in week of auditor's working day</param>
        private void SetWorkingDay(
            TimeSpan? workingDayStart, TimeSpan? workingDayEnd,
            int startWorkDay, int endWorkDay)
        {
            if (workingDayStart.HasValue)
                DayStartTime = workingDayStart.Value;
            if (workingDayEnd.HasValue)
                DayEndTime = workingDayEnd.Value;
            DayStartDay = startWorkDay;
            DayEndDay = endWorkDay;
        }

        /// <summary>
        /// Opens Chrono Calendar
        /// </summary>
        /// <param name="navigation">Navigation</param>
        private void ShowChronoPage(INavigation navigation)
        {
            calendarPage = new CalendarPage();
            calendarPage.viewModel.IsLoading = true;
            calendarPage.viewModel.GetChronoDataAction = GetChronoDataAction;
            this.navigation = navigation;
            navigation.PushAsync(calendarPage, false);
            Task.Run(async () =>
            {
                var (items, positon) = await ReloadTimeTrackerData(DateTime.Now);
                calendarPage.viewModel.ActivityItemsSource = items;
                calendarPage.viewModel.ManualSetPosition = positon;
                calendarPage.viewModel.CurrentDayItem = items[positon];
                calendarPage.viewModel.IsLoading = false;
            });
        }

        /// <summary>
        /// Loads new Chrono data 
        /// </summary>
        /// <param name="fromDate">Date from</param>
        /// <param name="toDate">Date to</param>
        /// <returns></returns>
        internal async Task<List<ChronoDayInfo>> GetChronoDataAction(DateTime fromDate, DateTime toDate)
        {
            return await Task.Run(() =>
            {
                var activities = new List<ChronoDayInfo>();//GetTimeTrackerActivities(fromDate, toDate);
                return activities;
            });
        }

        /// <summary>
        /// Updates time tracker data
        /// </summary>
        /// <param name="selectedDate">Selected Date for reload</param> 
        internal async Task<(List<ChronoDayInfo>, int)> ReloadTimeTrackerData(DateTime? selectedDate = null)
        {
            DateTime day = selectedDate != null ? selectedDate.Value.Date : DateTime.Today;
            
            DateTime activitiesStart = day < FirstLaunchDate.Date || day > DateTime.Today ? day : FirstLaunchDate.Date;
            DateTime activitiesEnd = day < FirstLaunchDate.Date || day > DateTime.Today ? day : DateTime.Today;
            
            var dates = Enumerable.Range(0, 1 + activitiesEnd.Subtract(activitiesStart).Days)
                .Select(offset => activitiesStart.AddDays(offset))
                .ToList();
            if (dates.Count > ChronoConstants.KeepDataStartDateLimit)
            {
                var maxAvailableDate = day.AddDays(ChronoConstants.KeepDataStartDateLimit / 2);
                var minAvailableDate = day.AddDays(-ChronoConstants.KeepDataStartDateLimit / 2);
                if (maxAvailableDate <= activitiesEnd && minAvailableDate >= activitiesStart)
                {
                    activitiesEnd = maxAvailableDate;
                    activitiesStart = minAvailableDate;
                }
                else if (minAvailableDate < activitiesStart && maxAvailableDate <= activitiesEnd)
                {
                    activitiesEnd = maxAvailableDate.AddDays((activitiesStart - minAvailableDate).TotalDays) < activitiesEnd ?
                        maxAvailableDate.AddDays((activitiesStart - minAvailableDate).TotalDays) : activitiesEnd;
                } else if (maxAvailableDate > activitiesEnd && minAvailableDate >= activitiesStart)
                {
                    activitiesStart = minAvailableDate.AddDays((activitiesEnd - maxAvailableDate).TotalDays) > activitiesStart ?
                        minAvailableDate.AddDays((activitiesEnd - maxAvailableDate).TotalDays) : activitiesStart;
                }
            }
            dates = Enumerable.Range(0, 1 + activitiesEnd.Subtract(activitiesStart).Days)
                .Select(offset => activitiesStart.AddDays(offset))
                .ToList();
            int position = dates.IndexOf(day);
            return (GetTimeTrackerActivities(activitiesStart, activitiesEnd), position);
        }

        /// <summary>
        /// Gets Time Tracker Activities from database
        /// </summary>
        /// <param name="dateFrom">From date</param>
        /// <param name="dateTo">To date</param>
        /// <returns>List of Days Information</returns>
        private List<ChronoDayInfo> GetTimeTrackerActivities(DateTime dateFrom, DateTime dateTo)
        {
            var daysInfo = new List<ChronoDayInfo>();
            dateFrom = dateFrom.Add(-(dateFrom.TimeOfDay));
            dateTo = dateTo.Add(TimeSpan.FromHours(24) - dateTo.TimeOfDay);

            var savedActivities = new List<ChronoActivityInfo>();//ChronoDatabase.GetChronoActivities(instructionPath, dateFrom, dateTo, null);
            /*if (savedActivities != null && savedActivities.Any())
            {
                savedActivities = FillActivityTitles(savedActivities);
            }*/
            for (var date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                var dayInfo = ParseIntervals(savedActivities, date);
                daysInfo.Add(dayInfo);
            }
            return daysInfo;
        }

        /// <summary>
        /// Parses the intervals.
        /// </summary>
        /// <returns>The intervals.</returns>
        /// <param name="activities">Activities.</param>
        /// <param name="date">Date.</param>
        private ChronoDayInfo ParseIntervals(IReadOnlyCollection<ChronoActivityInfo> activities,
            DateTime date)
        {
            var maxEnabledActTime = DateTime.Now;
            var intervals = new List<TimeInterval>();
            ParseActivities(activities, date,
                (dayAct, start, end) =>
                {
                    return AddEmptyIntervals(dayAct, intervals, start, end, date);
                },
                (start, end, activity) =>
                {
                    var mode = IntervalMode.Readonly;
                    if (!activity.IsManualActivity && !activity.IsExported)
                    {
                        mode = IntervalMode.Readonly;
                    }
                    else if (activity.IsExported)
                    {
                        mode = IntervalMode.Exported;
                    }
                    else
                    {
                        mode = IntervalMode.Editable;
                    }
                    intervals.Add(CreateTimeInterval(date, start, end, mode, activity));
                });

            return new ChronoDayInfo(date, intervals);
        }

        /// <summary>
        /// Parses the activities.
        /// </summary>
        /// <param name="activities">Activities.</param>
        /// <param name="date">Date.</param>
        /// <param name="gapAction">Gap action.</param>
        /// <param name="activityAction">Activity action.</param>
        private void ParseActivities(IReadOnlyCollection<ChronoActivityInfo> activities,
            DateTime date,
            TimeGapHandler gapAction,
            Action<TimeSpan, TimeSpan, ChronoActivityInfo> activityAction = null)
        {
            var nextDate = date.AddDays(1);
            var dayActivities = activities == null ? null :
                (from a in activities
                 where ((a.StartTime >= date &&
                     a.StartTime < nextDate) ||
                     (a.EndTime > date &&
                         a.EndTime < nextDate) ||
                     (a.StartTime < date &&
                         a.EndTime >= nextDate))
                 select a).ToList();
            if (dayActivities != null && dayActivities.Count > 0)
            {
                DateTime startDateTime, endDateTime;
                TimeSpan startTime = TimeSpan.Zero;
                TimeSpan endTime = TimeSpan.Zero;

                DateTime endFilledDateTime = dayActivities.OrderByDescending(s => s.EndTime).First().EndTime;
                TimeSpan endFilledTime = endFilledDateTime.Date > date ?
                    new TimeSpan(date.Day, 23, 59, 59) :
                    new TimeSpan(endFilledDateTime.Day, endFilledDateTime.Hour, endFilledDateTime.Minute, endFilledDateTime.Second);

                Action<ChronoActivityInfo> updateStartTime = info =>
                {
                    startDateTime = info.StartTime <= date ? date : info.StartTime;
                    startTime = startDateTime - startDateTime.Date;
                };

                Action<ChronoActivityInfo> updateEndTime = info =>
                {
                    endDateTime = info.EndTime >= nextDate ? nextDate.AddSeconds(-1) : info.EndTime;
                    endTime = endDateTime - endDateTime.Date;
                };

                dayActivities = dayActivities.OrderBy(s => s.EndTime).ToList();
                updateStartTime(dayActivities[0]);
                TimeSpan lastTime = startTime < DayStartTime ?
                    startTime : DayStartTime;
                for (int i = 0; i < dayActivities.Count && lastTime <= MaxTime; i++)
                {
                    updateStartTime(dayActivities[i]);
                    updateEndTime(dayActivities[i]);

                    if (startTime > endTime)
                    {
                        continue;
                    }

                    if (startTime > lastTime)
                    {
                        gapAction(dayActivities, lastTime, startTime);
                        lastTime = startTime;
                    }
                    else if (startTime < lastTime && endTime >= lastTime)
                    {
                        activityAction(startTime, endTime - startTime, dayActivities[i]);
                        lastTime = lastTime > endTime ? lastTime : endTime;
                    }
                    else if (startTime < lastTime && endTime < lastTime)
                    {
                        lastTime = endTime < endFilledTime ? endTime : endFilledTime;
                        activityAction(startTime, lastTime - startTime, dayActivities[i]);
                    }

                    // if startTime < lastTime then ignore invalid intervals
                    if (startTime == lastTime)
                    {
                        lastTime = endTime < endFilledTime ? endTime : endFilledTime;
                        activityAction(startTime, lastTime - startTime, dayActivities[i]);
                    }
                }
                gapAction(dayActivities, new TimeSpan(endFilledTime.Hours, endFilledTime.Minutes, endFilledTime.Seconds), DayEndTime);
            }
            else
            {
                gapAction(dayActivities, DayStartTime, DayEndTime);
            }
        }

        /// <summary>
        /// Creates time interval
        /// </summary>
        /// <returns></returns>
        private TimeInterval CreateTimeInterval(DateTime date, TimeSpan startTime, TimeSpan duration, IntervalMode mode, ChronoActivityInfo activityInfo)
        {
            var interval = new TimeInterval(date, startTime, duration, mode, activityInfo);

            return interval;
        }        

        /// <summary>
        /// Adds the empty intervals.
        /// </summary>
        /// <returns><c>true</c>, if empty intervals was added, <c>false</c> otherwise.</returns>
        /// <param name="dayActivities">List of day activities</param>
        /// <param name="intervals">Intervals.</param>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="date">Date.</param>
        private bool AddEmptyIntervals(List<ChronoActivityInfo> dayActivities, List<TimeInterval> intervals, TimeSpan start, TimeSpan end, DateTime date)
        {
            var res = IsEmptyIntervals(start, end, dayActivities, date);
            if (res)
            {
                for (TimeSpan tStart = start; tStart <= end; tStart = tStart.Add(TimeStep))
                {
                    var tEnd = tStart.Add(TimeStep);
                    if (tEnd > end)
                    {
                        tEnd = end;
                    }
                    if ((tEnd - tStart).TotalSeconds >= MinActivityDurationSec)
                    {
                        var intervalMode = DateTime.Now < date.Date.AddTicks(tEnd.Ticks) || date.Date < FirstLaunchDate.Date ?
                                    IntervalMode.EmptyDisabled : IsIntervalWorking(tStart, tEnd, date) ?
                                    IntervalMode.Empty :
                                    IntervalMode.EmptyOptional;
                        intervals.Add(new TimeInterval(date, tStart, tEnd - tStart, intervalMode));
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Determines if is empty intervals the specified start end.
        /// </summary>
        /// <returns><c>true</c> if is empty intervals the specified start end; otherwise, <c>false</c>.</returns>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        /// <param name="dayActivities">List of day activities</param>
        /// <param name="date">Date</param>
        private bool IsEmptyIntervals(TimeSpan start, TimeSpan end, List<ChronoActivityInfo> dayActivities, DateTime date)
        {
            bool isEmpty = (end - start).TotalSeconds >= MinActivityDurationSec;
            var endTime = end.Add(TimeSpan.FromSeconds(-1));
            var startTime = start.Add(TimeSpan.FromSeconds(1));
            if (dayActivities != null && dayActivities.Any() && isEmpty)
            {
                var filledAct = dayActivities.Where(act => (act.StartTime.TimeOfDay <= startTime &&
                    act.EndTime.TimeOfDay >= endTime && act.StartTime.Date == date && act.EndTime.Date == date)
                    || (act.StartTime.Date < date && act.EndTime.Date == date && act.EndTime.TimeOfDay >= endTime)
                    || (act.EndTime.Date > date && act.StartTime.Date == date && act.StartTime.TimeOfDay <= startTime)
                    || (act.EndTime.Date > date && act.StartTime.Date < date));

                isEmpty = (filledAct == null || (filledAct != null && !filledAct.Any()));
            }
            return isEmpty;
        }

        /// <summary>
        /// Initializes first launch date
        /// </summary>
        private void IntilaizeDefaultFirstLaunchDate()
        {
            FirstLaunchDate = DateTime.Now.AddDays(-5);
        }

        /// <summary>
        /// Gets the previous business day.
        /// </summary>
        /// <returns>The previous business day.</returns>
        /// <param name="date">Date.</param>
        private DateTime GetPrevBusinessDay(DateTime prevDate)
        {
            var date = prevDate.Date.AddDays(-1);
            while (!IsDayWorking(date.DayOfWeek))
            {
                date = date.AddDays(-1);
            }
            return date;
        }

        /// <summary>
        /// Defines if day working or not
        /// </summary>
        /// <param name="day">Day of week</param>
        /// <returns>><c>true</c> if day is working day, otherwise <c>false</c></returns>
        private bool IsDayWorking(DayOfWeek day)
        {
            return (int)day >= DayStartDay && (int)day <= DayEndDay;
        }

        /// <summary>
        /// Defines if time interval is working or not
        /// </summary>
        /// <returns>><c>true</c> if interval is in working time, otherwise <c>false</c></returns>
        /// <param name="startTime">Interval start time</param>
        /// <param name="endTime">Interval end time</param>
        /// <param name="date">Interval date</param>
        private bool IsIntervalWorking(TimeSpan startTime, TimeSpan endTime, DateTime date)
        {
            var result = false;
            if (IsDayWorking(date.DayOfWeek))
            {
                result = startTime.Add(TimeSpan.FromSeconds(1)) > DayStartTime && endTime.Add(TimeSpan.FromSeconds(-1)) < DayEndTime
                    || startTime < DayStartTime && endTime > DayStartTime
                    || startTime < DayEndTime && endTime > DayEndTime;
            }
            return result;
        }
       

        /// <summary>
        /// Show Inactivity Dialog
        /// </summary>
        /// <param name="startInactivityTime">Staret time of inactivity</return>
        private void ShowInactivityDialog(DateTime startInactivityTime)
        {
            bool isInactivityAlreadyShow = false;
            
            if (!isInactivityAlreadyShow)
            {
                if (IsShowAsNative())
                {
                    ShowLongInactivityDialogNative(null, startInactivityTime, null);
                }
                else
                {
                    ShowLongInactivyDialog(null, startInactivityTime, null);
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// Get Time from string  
        /// </summary>
        /// <returns>End working time</returns>
        public TimeSpan? ConvertTime(string strTime)
        {
            TimeSpan? result = null;
            string[] name = strTime?.Split(':');
            if (name.Length == 2)
            {
                if (int.TryParse(name[0], out int hour) && int.TryParse(name[1], out int min))
                {
                    result = new TimeSpan(hour, min, 0);
                }
            }
            return result;
        }

        /// <summary>
        /// Get Day of week from string  
        /// </summary>
        /// <returns>End working time</returns>
        public int ConvertDay(string strDay)
        {
            int result = 0;
            switch (strDay.Trim().ToLower())
            {
                case "monday":
                    result = 1;
                    break;
                case "tuesday":
                    result = 2;
                    break;
                case "wednesday":
                    result = 3;
                    break;
                case "thursday":
                    result = 4;
                    break;
                case "friday":
                    result = 5;
                    break;
                case "saturday":
                    result = 6;
                    break;
                case "sunday":
                    result = 0;
                    break;
                default:
                    result = 1;
                    break;
            }
            return result;
        }
        #endregion
    }
}