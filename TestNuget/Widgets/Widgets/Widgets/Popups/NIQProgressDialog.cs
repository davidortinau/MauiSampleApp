using Widgets.Models;
using static Widgets.Enumerations;

namespace Widgets
{
    /// <summary>
    /// Progress alert view
    /// </summary>
    public class NIQProgressDialog : NIQDialog
    {
        #region Private methods
        private readonly NIQProgressBar progressBarView;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQProgressDialog"/> class
        /// </summary>
        /// <param name="dialogTitle">Alert title</param>
        /// <param name="actions">Actions</param>
        /// <param name="maxProgress">Maximal progress value</param>
        public NIQProgressDialog(string dialogTitle, List<ButtonInfo> actions, int maxProgress = 1)
            : base(dialogTitle, actions, view: null, dialogType: NIQDialogType.Information)
        {
            progressBarView = new NIQProgressBar(maxProgress);
            MaxProgress = maxProgress;

            SetContentView(progressBarView);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message
        {
            get => progressBarView.Message;
            set => progressBarView.Message = value;
        }

        /// <summary>
        /// Gets or sets progress
        /// </summary>
        public int MaxProgress
        {
            get => progressBarView.MaxProgress;
            set => progressBarView.MaxProgress = value;
        }

        /// <summary>
        /// Gets or sets progress
        /// </summary>
        public int Progress
        {
            get => progressBarView.Progress;
            set => progressBarView.Progress = value;
        }
        #endregion
    }
}

