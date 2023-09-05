using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

[assembly: ExportRenderer(typeof(Widgets.NIQDarkPopupItem), typeof(Widgets.Droid.NIQDarkPopupItemRenderer))]
namespace Widgets.Droid
{
    /// <summary>
    /// NIQ Popup item renderer
    /// </summary>
    public class NIQDarkPopupItemRenderer : LabelRenderer
    {
        /// <summary>
        /// Creates new instance of <see cref="NIQPopupItemRenderer"/>
        /// </summary>
        /// <param name="context">Context</param>
        public NIQDarkPopupItemRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// On element changed handler
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Touch += OnTouch;
            }
        }

        /// <summary>
        /// OnTouch event handler
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event args</param>
        private void OnTouch(object sender, TouchEventArgs e)
        {
            MotionEventActions action = e.Event.Action;
            if (action == MotionEventActions.Down)
            {
                Element.BackgroundColor = NIQThemeController.Theme.PrimaryVariant;
            }
            else if (action == MotionEventActions.Cancel || action == MotionEventActions.Up)
            {
                Element.BackgroundColor = Colors.Transparent;
            }
            OnTouchEvent(e.Event);
        }
    }
}