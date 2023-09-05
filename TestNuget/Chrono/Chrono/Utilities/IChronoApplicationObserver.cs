namespace Chrono
{
    /// <summary>
    /// Chrono Application Observer Interface
    /// </summary>
    public interface IChronoApplicationObserver
    {
        /// <summary>
        /// It called when chrono dialog shall be closed
        /// </summary>
        void OnCloseFormsApplication();

        /// <summary>
        /// Is called when Forms application shall be closed.
        /// </summary>
        void OnCloseDialog();

        /// <summary>
        /// It called when top chrono dialog fragment shall be closed
        /// </summary>
        void OnCloseDialogFragment();
    }
}
