namespace Chrono
{
    /// <summary>
    /// Chrono Long Activity Info
    /// </summary>
    public class ChronoLongActivityInfo
    {
        #region Properties
        /// <summary>
        /// Long activity start time
        /// </summary>
        public DateTime ActivityStartTime { get; set; }

        /// <summary>
        /// Long activity end time
        /// </summary>
        public DateTime ActivityEndTime { get; set; }

        /// <summary>
        /// Activity guids list
        /// </summary>
        public List<Guid> Guids { get; set; }
        #endregion
    }
}
