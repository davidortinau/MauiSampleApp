using Widgets.Models;

namespace Widgets
{
    /// <summary>
    /// Progress dialog wrapper
    /// </summary>
    public class NIQProgressDialogWrapper
    {
        #region Private members
        private readonly Label messageLabel;
        private readonly ProgressBar progressBar;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="NIQProgressDialogWrapper"/>
        /// </summary>
        /// <param name="rootDialog">Root dialog</param>
        /// <param name="actions">List of actions</param>
        /// <param name="maxProgress">Max progress</param>
        public NIQProgressDialogWrapper(NIQDialog rootDialog, List<ButtonInfo> actions, int maxProgress = 1)
        {
            MaxProgress = maxProgress;

            progressBar = new ProgressBar();
            messageLabel = new Label();
            rootDialog.SetActions(actions);
            rootDialog.SetContentView(new List<View>()
            {
                messageLabel,
                progressBar
            });
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets message
        /// </summary>
        public string Message
        {
            get { return (string)messageLabel.GetValue(Label.TextProperty); }
            set { messageLabel.SetValue(Label.TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets progress
        /// </summary>
        public int MaxProgress { get; set; }

        /// <summary>
        /// Gets or sets progress
        /// </summary>
        public int Progress
        {
            get { return (int)progressBar.Progress; }
            set { progressBar.ProgressTo(value / (float)MaxProgress, 250, Easing.Linear); }
        }
        #endregion
    }
}
