namespace Chrono.Utilities
{

    /// <summary>
    /// Chrono Native Helper Interface
    /// </summary>
    public interface IChronoNativeHelper
    {
        /// <summary>
        /// Opens Chrono Calendar Page
        /// </summary>
        public void OpenChronoCalendarPage();

        /// <summary>
        /// Shows chrono atypical activity dialog
        /// </summary>
        /// <param name="auditInfo">Audit info</param>
        /// <param name="inactivityStartTime">Inactivity start time</param>
        /// <param name="selectedAction">Action to invoke on some action performed from dialog</param>
        public void ShowChronoLongInactivityDialog(ChronoAuditInfo auditInfo, DateTime inactivityStartTime, Action<ChronoManager.ChronoNewActivityActionType> selectedAction);
    }
}
