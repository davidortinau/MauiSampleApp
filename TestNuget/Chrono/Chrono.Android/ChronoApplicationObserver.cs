using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

[assembly: Dependency(typeof(Chrono.Droid.ChronoApplicationObserver))]
namespace Chrono.Droid
{
    public class ChronoApplicationObserver : IChronoApplicationObserver
    {
        /// <summary>
        /// Is called when Forms application shall be closed.
        /// </summary>
        public void OnCloseFormsApplication()
        {
            var activity = Platform.CurrentActivity as ChronoActivity;
            activity?.Finish();
        }

        /// <summary>
        /// It called when chrono dialog shall be closed
        /// </summary>
        public void OnCloseDialog()
        {
            var activity = Platform.CurrentActivity as ChronoDialogActivity;
            activity.ShowActivityIndicator();
            activity?.Finish();
        }

        /// <summary>
        /// It called when top chrono dialog fragment shall be closed
        /// </summary>
        public void OnCloseDialogFragment()
        {
            var activity = Platform.CurrentActivity as ChronoDialogActivity;
            activity?.CloseDialogFragment();
        }
    }
}