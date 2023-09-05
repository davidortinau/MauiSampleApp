using ChronoDatabase.Models;

namespace Chrono
{
    /// <summary>
    /// Interval mode
    /// </summary>
    public enum IntervalMode
    {
        Editable,
        Readonly,
        Exported,
        Empty,
        EmptyDisabled,
        EmptyOptional
    }

    /// <summary>
    /// Day fill type.
    /// </summary>
    public enum DayFillType
    {
        Empty,
        Partial,
        Full
    }

    /// <summary>
    /// Time Interval
    /// </summary>
    public class TimeInterval
    {
        #region Public properties
        /// <summary>
        /// Gets Time Interval Start Time
        /// </summary>
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets Time Interval Duration
        /// </summary>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Gets Time Interval Mode
        /// </summary>
        public IntervalMode IntervalMode { get; private set; }

        /// <summary>
        /// Gets Activity Info for time interval
        /// </summary>
        public ChronoActivityInfo ActivityInfo { get; private set; }

        /// <summary>
        /// Gets Time Interval End Time
        /// </summary>
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets Time Interval Date
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets or sets the value indicating if time interval conflicting with other
        /// </summary>
        public bool HasConflict { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if time interval is selected or not
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the value indicating whenever this activity is long activity or not
        /// </summary>
        public bool IsLongActivity { get; set; }

        /// <summary>
        /// Gets or sets long activity info
        /// </summary>
        public ChronoLongActivityInfo LongActivityInfo {get; set;}
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes the instance of <see cref="TimeInterval.cs"/>
        /// </summary>
        /// <param name="date">Time interval date</param>
        /// <param name="startTime">Time interval start time</param>
        /// <param name="duration">Time interval duration</param>
        /// <param name="intervalMode">Interval mode</param>
        /// <param name="activityInfo">Interval activity info</param>
        public TimeInterval(DateTime date, TimeSpan startTime, TimeSpan duration, IntervalMode intervalMode,
            ChronoActivityInfo activityInfo)
        {
            Date = date;
            StartTime = startTime;
            Duration = duration;
            IntervalMode = intervalMode;
            activityInfo.ActivityTitle = activityInfo.ActivityTitle;
            ActivityInfo = activityInfo;
            EndTime = startTime + duration;
            HasConflict = false;
            IsSelected = false;
        }

        /// <summary>
        /// Initializes the instance of <see cref="TimeInterval.cs"/>
        /// </summary>
        /// <param name="date">Time interval date</param>
        /// <param name="startTime">Time interval start time</param>
        /// <param name="duration">Time interval duration</param>
        /// <param name="activityMode">Interval mode</param>
        public TimeInterval(DateTime date, TimeSpan startTime, TimeSpan duration, IntervalMode activityMode)
        {
            Date = date;
            StartTime = startTime;
            Duration = duration;
            IntervalMode = activityMode;
            EndTime = startTime + duration;
            HasConflict = false;
            IsSelected = false;
        }
        #endregion
    }
}

