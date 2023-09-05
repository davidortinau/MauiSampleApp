using Microsoft.Maui.Embedding;
using Microsoft.Maui;
using Widgets;
using Widgets.Models;
using static Widgets.Enumerations;

namespace Chrono
{
    /// <summary>
    /// Chrono Dialog Page
    /// </summary>
    public class NIQChronoDialog : NIQDialog
    {
        /// <summary>
        /// Value indicating that NIQChronoDialog is active
        /// </summary>
        public bool IsActive { get; private set; }

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQChronoDialog"/> class
        /// </summary>
        /// <param name="dialogTitle">Dialog title</param>
        /// <param name="actions">List of actions</param>
        /// <param name="contentView">List of views</param>
        /// <param name="dialogType">Dialog Type. Can be skipped to hide any icons</param>
        /// <param name="logMessage">Log Message</param>
        /// <param name="titleButtonInfo">Title button info</param>
        public NIQChronoDialog(
            string dialogTitle, List<ButtonInfo> actions, List<View> contentView, 
            Enumerations.NIQDialogType dialogType = Enumerations.NIQDialogType.None,
            string logMessage = null, DialogTitleButtonInfo titleButtonInfo = null, bool isNative = false) 
            : base(dialogTitle, actions, contentView, dialogType, logMessage, titleButtonInfo)
        {
            if (isNative)
            {
                Parent = Application.Current ?? new ChronoApp(true);
            }
            IsNotCloseByBack = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NIQDialog"/> class
        /// </summary>
        /// <param name="dialogTitle">Dialog title</param>
        /// <param name="actions">List of actions</param>
        /// <param name="view">Content views</param>
        /// <param name="dialogType">Dialog Type. Can be skipped to hide any icons</param>
        /// <param name="logMessage">Log Message</param>
        /// <param name="titleButtonInfo">Title button info</param>
        public NIQChronoDialog(string dialogTitle, List<ButtonInfo> actions, View view,
            NIQDialogType dialogType = NIQDialogType.None, string logMessage = null,
            DialogTitleButtonInfo titleButtonInfo = null, bool isNative = false)
            : base(dialogTitle, actions, view, dialogType, logMessage, titleButtonInfo)
        {
            if (isNative)
            {
                Parent = Application.Current ?? new ChronoApp(true);
            }
            IsNotCloseByBack = true;
        }
        #endregion

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