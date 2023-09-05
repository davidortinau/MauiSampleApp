namespace Utilities
{
    public interface IDateTimeFormat
    {
        /// <summary>
        /// Gets settings date separator
        /// </summary>
        char DateSettingSeparator { get; }

        /// <summary>
        /// Gets allowed date formats
        /// </summary>
        IEnumerable<string> AllowedDateFormats { get; }

        /// <summary>
        /// Gets or sets date format
        /// </summary>
        string DateFormat { get; set; }

        /// <summary>
        /// Gets short date format (without year)
        /// </summary>
        string ShortDateFormat { get; }

        /// <summary>
        /// Gets date with time format
        /// </summary>
        string DateTimeFormat { get; }

        /// <summary>
        /// Gets short date with time format (without year)
        /// </summary>
        string ShortDateTimeFormat { get; }

        /// <summary>
        /// Gets or sets time format
        /// </summary>
        string TimeFormat { get; set; }

        // <summary>
        /// Returns a provided date separator according to settings.
        /// </summary>
        char GetDateSeparator();

        /// <summary>
        /// Returns a provided datetime (date part only) formatted according to settings.
        /// </summary>
        string GetDate(DateTime timestamp);

        /// <summary>
        /// Returns a provided datetime (time part only) formatted according to settings.
        /// </summary>
        string GetTime(DateTime timestamp);

        /// <summary>
        /// Returns provided datetime (date part only without year) formatted according to settings.
        /// </summary>
        string GetShortDate(DateTime timestamp);

        /// <summary>
        /// Returns provided datetime formatted according to settings.
        /// </summary>
        string GetDateTime(DateTime timestamp);

        /// <summary>
        /// Returns provided datetime (without year) formatted according to settings.
        /// </summary>
        string GetShortDateTime(DateTime timestamp);

        /// <summary>
        /// Returns provided datetime (for technical purposes) formatted according to settings.
        /// </summary>
        string GetFullDateTime(DateTime timestamp);
    }
}
