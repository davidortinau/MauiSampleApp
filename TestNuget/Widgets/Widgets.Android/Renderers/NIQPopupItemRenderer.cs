using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Platform;
using Widgets;
using Widgets.Droid;

[assembly: Microsoft.Maui.Controls.Compatibility.ExportRenderer(typeof(NIQPopupItem), typeof(NIQPopupItemRenderer))]
namespace Widgets.Droid
{
    /// <summary>
    /// NIQ Popup item renderer
    /// </summary>
    public class NIQPopupItemRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ViewRenderer<NIQPopupItem, AndroidX.AppCompat.Widget.AppCompatTextView>
    {
        /// <summary>
        /// Creates new instance of <see cref="NIQPopupItemRenderer"/>
        /// </summary>
        /// <param name="context">Context</param>
        public NIQPopupItemRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// On element changed handler
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnElementChanged(ElementChangedEventArgs<NIQPopupItem> e)
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
                Element.BackgroundColor = NIQThemeController.Theme.HoverColor;
                Element.TextColor = NIQThemeController.Theme.OnSecondary;
            }
            else if (action == MotionEventActions.Cancel || action == MotionEventActions.Up)
            {
                Element.BackgroundColor = Colors.Transparent;
                Element.TextColor = NIQThemeController.Theme.OnBackground;
            }
            OnTouchEvent(e.Event);
        }
    }
}