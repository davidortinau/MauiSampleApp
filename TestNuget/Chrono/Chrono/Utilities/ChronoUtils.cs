using Utilities;
using System.Text.RegularExpressions;

namespace Chrono.Utilities
{
    /// <summary>
    /// Chrono Utilitis class
    /// </summary>
    public class ChronoUtils
    {
        private const string TAG = nameof(ChronoUtils);

        /// <summary>
        /// To the mm.dd.yy long string.
        /// </summary>
        /// <param name="dt">Date time which need to format</param>
        /// <returns>The mm.dd.yy string.</returns>
        public static string ToMonthDayYearLongString(DateTime dt)
        {
            string dateSeparator =
                System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator;
            string dateFormat =
                System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.LongDatePattern;

            dateFormat = dateFormat.Replace("dddd", "");
            dateFormat = Regex.Replace(dateFormat, string.Format("^[{0}, ]*", dateSeparator), "");
            dateFormat = Regex.Replace(dateFormat, string.Format("[{0}, ]*$", dateSeparator), "");
            return dt.ToString(dateFormat.Trim());
        }

        /// <summary>
        /// Gets the time zone pattern value.
        /// </summary>
        /// <returns>The time zone pattern value.</returns>
        private static string GetTimeZonePatternValue()
        {
            var offset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            return string.Format("\"TimeZone\":\"UTC{0}{1:hh\\:mm}\", \"TimeZoneName\":\"{2}\"",
                offset < TimeSpan.Zero ? "-" : "+",
                offset,
                TimeZoneInfo.Local.DisplayName);
        }

        /// <summary>
        /// Gets Date Time based on date and time
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="time">Time</param>
        /// <returns></returns>
        internal static DateTime GetDateTime(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }

        /// <summary>
        /// Get datetime in hours and minutes.
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <param name="time">Time to format</param>
        /// <returns>dateTime in format year.month.day hours:minutes</returns>
        public static DateTime GetDateTimeInHourMinute(DateTime date, TimeSpan time)
        {
            var hm = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, 0);
            return hm;
        }
    }
}
