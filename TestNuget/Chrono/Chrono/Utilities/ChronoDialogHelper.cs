using Utilities;
using Widgets.Models;
using static Chrono.ChronoManager;
using static Widgets.Enumerations;
using WidgetsUtils = Widgets.Utilities;
using Widgets;

namespace Chrono
{
    /// <summary>
    /// Chrono Dialog Helper
    /// </summary>
    public static class ChronoDialogHelper
    {
        #region Constants
        private const string TAG = nameof(ChronoDialogHelper);
        #endregion

        /// <summary>
        /// Creates chrono atypical dialog
        /// </summary>
        /// <param name="auditInfo">Audit Info</param>
        /// <param name="inactivityStartTime">Inactivity Start Time</param>
        /// <param name="selectedAction">Selected Action</param>
        /// <returns></returns>
        public static NIQChronoDialog ShowLongInactivyDialog(
            ChronoAuditInfo auditInfo, DateTime inactivityStartTime,
            Action<ChronoNewActivityActionType> selectedAction, bool isNative = false)
        {
            var manualAction = new ButtonInfo()
            {
                ButtonType = ButtonType.Positive,
                Title = MCOEStringResources.Manually,
                OnClicked = (view) =>
                {
                    if (isNative)
                    {
                        DependencyService.Get<IChronoApplicationObserver>().OnCloseDialog();
                    }
                    else
                    {
                        var lastPage = WidgetsUtils.GetLastPage();
                        _ = lastPage.Navigation.PopModalAsync(false);
                    }
                }
            };
            var autoAction = new ButtonInfo()
            {
                ButtonType = ButtonType.Positive,
                Title = MCOEStringResources.Auto,
                OnClicked = (view) =>
                {
                    selectedAction?.Invoke(ChronoNewActivityActionType.LongAtypical);
                    if (isNative)
                    {
                        DependencyService.Get<IChronoApplicationObserver>().OnCloseDialog();
                    }
                    else
                    {
                        var lastPage = WidgetsUtils.GetLastPage();
                        _ = lastPage.Navigation.PopModalAsync(false);
                    }
                }
            };

            List<ButtonInfo> buttonList = new List<ButtonInfo>() { manualAction, autoAction };

            return new NIQChronoDialog(MCOEStringResources.DialogTitle,
                buttonList, new Label()
                {
                    Text = MCOEStringResources.DialogMessage,
                    TextColor = NIQThemeController.Theme.OnSecondary
                },
                dialogType: NIQDialogType.Warning, isNative: isNative);
        }
    }
}
