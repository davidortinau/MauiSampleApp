using Android.Content;
using Newtonsoft.Json;
using Chrono.Utilities;
using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

[assembly: Dependency(typeof(Chrono.Droid.ChronoNativeHelper))]
namespace Chrono.Droid
{
    /// <summary>
    /// Chrono Native Dialogs Actions
    /// </summary>
    public static class ChronoNativeDialogsBridge
    {
        public static Action<ChronoManager.ChronoNewActivityActionType> SelectedAction { get; set; }

        public static NIQChronoDialog ChronoDialog { get; set; }
    }

    /// <summary>
    /// Chrono Native Helper
    /// </summary>
    public class ChronoNativeHelper : IChronoNativeHelper
    {
        /// <summary>
        /// Opens Chrono Calendar Page
        /// </summary>
        public void OpenChronoCalendarPage()
        {
            var ctx = Platform.CurrentActivity;
            var intent = new Intent(ctx, typeof(ChronoLoadingActivity));
            intent.PutExtra(ChronoActivity.ExtraKeyChronoActivityMode, (int)ChronoActivity.ChronoActivityMode.Calendar);
            ctx.StartActivity(intent);
        }

        /// <summary>
        /// Shows chrono atypical activity dialog
        /// </summary>
        /// <param name="auditInfo">Audit info</param>
        /// <param name="inactivityStartTime">Inactivity start time</param>
        /// <param name="selectedAction">Action to invoke on some action performed from dialog</param>
        public void ShowChronoLongInactivityDialog(ChronoAuditInfo auditInfo, DateTime inactivityStartTime, Action<ChronoManager.ChronoNewActivityActionType> selectedAction)
        {
            var ctx = Platform.CurrentActivity;
            ChronoNativeDialogsBridge.SelectedAction = selectedAction;
            var intent = new Intent(ctx, typeof(ChronoDialogActivity));
            intent.PutExtra(ChronoDialogActivity.ExtraKeyInactivityDateTime, inactivityStartTime.ToString());
            intent.PutExtra(ChronoDialogActivity.ExtraKeyAuditInfo, JsonConvert.SerializeObject(auditInfo));
            intent.PutExtra(ChronoDialogActivity.ExtraDialogMode, (int)ChronoDialogActivity.ChronoShowDialogMode.Inactivity);
            ctx.StartActivity(intent);
        }
    }
}