using Widgets;

namespace Chrono
{
    /// <summary>
    /// Chrono Base Page
    /// </summary>
    public class ChronoBasePage : NIQContentPage
    {
        /// <summary>
        /// Value indicating that NIQChronoDialog is active
        /// </summary>
        public bool IsActive { get; private set; }

        // <summary>
        /// Allows application developers to customize behavior immediately prior to the Page becoming visible
        /// </summary>
        protected override void OnAppearing()
        {
            ChronoManager.Instance.IsChronoOnTop = true;
            IsActive = true;
            base.OnAppearing();
        }

        /// <summary>
        /// Allows the application developer to customize behavior as the Xamarin.Forms.Page disappears
        /// </summary>
        protected override void OnDisappearing()
        {
            ChronoManager.Instance.IsChronoOnTop = false;
            IsActive = false;
            base.OnDisappearing();
        }
    }
}