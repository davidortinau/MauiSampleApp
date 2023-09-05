using System.ComponentModel;

namespace Chrono
{
    /// <summary>
    /// Chrono Day Info
    /// </summary>
    public class ChronoDayInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        #region Public properties
        /// <summary>
        /// Gets or sets the date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets Time Interval Source
        /// </summary>
        public List<TimeInterval> TimeIntervals { get; private set; }
        #endregion

        /// <summary>
        /// Initializes the instance of <see cref="ChronoDayInfo.cs"/>
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="timeIntervals">Time intervals list for the date</param>
        public ChronoDayInfo(DateTime date, List<TimeInterval> timeIntervals)
        {
            Date = date;
            TimeIntervals = timeIntervals;
        }

        /// <summary>
        /// Updates time intervals
        /// </summary>
        /// <param name="timeIntervals">Time intervals</param>
        public void UpdateTimeIntervals(List<TimeInterval> timeIntervals)
        {
            TimeIntervals = new List<TimeInterval>(timeIntervals);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeIntervals)));
        }
    }
}

