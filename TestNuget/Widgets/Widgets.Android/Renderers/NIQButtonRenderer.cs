using Android.Content;
using Android.Views;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Widgets;
using Widgets.Droid;

[assembly: ExportRenderer(typeof(NIQButton), typeof(NIQButtonRenderer))]
namespace Widgets.Droid
{
    /// <summary>
    /// Button renderer
    /// </summary>
    public class NIQButtonRenderer : Microsoft.Maui.Controls.Handlers.Compatibility.ViewRenderer<NIQButton, AndroidX.AppCompat.Widget.AppCompatButton>
    {
        public NIQButtonRenderer(Context context) : base(context)
        { }

        /// <summary>
        /// On element changed
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnElementChanged(ElementChangedEventArgs<NIQButton> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                Control.Touch -= OnTouch;
                Control.Touch += OnTouch;

                if (e.NewElement.Parent.Parent is NIQButton button)
                {
                    Control.SetMaxLines(button.IsMultilineAllowed ? 2 : 1);
                }
                Control.Ellipsize = Android.Text.TextUtils.TruncateAt.Middle;

                if (e.NewElement.Parent.Parent is NIQBorderlessButton borderlessButton &&
                    borderlessButton.IsUnderlinedTextAllowed)
                {
                    Control.Gravity = borderlessButton.IsTextAligmentStart ? GravityFlags.Start | GravityFlags.CenterVertical : GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;
                    if (NIQThemeController.Theme == Theme.NIQDark || NIQThemeController.Theme == Theme.NIQLight)
                    {
                        Control.PaintFlags = Android.Graphics.PaintFlags.UnderlineText;
                    }
                }
            }
        }

        /// <summary>
        /// Handles Touch event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Args</param>
        private void OnTouch(object sender, TouchEventArgs args)
        {
            var buttonController = (IButtonController)Element;
            if (buttonController == null)
                return;

            var x = (int)args.Event.GetX();
            var y = (int)args.Event.GetY();

            if (!TouchInsideControl(x, y))
            {
                buttonController.SendReleased();
            }
            else if (args.Event.Action == MotionEventActions.Down)
            {
                buttonController.SendPressed();
            }
            else if (args.Event.Action == MotionEventActions.Up)
            {
                buttonController.SendReleased();
                buttonController.SendClicked();
            }
        }

        /// <summary>
        /// Check if touch coordinates are inside control
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>True if user touched inside control, false otherwise</returns>
        private bool TouchInsideControl(int x, int y)
        {
            return x <= Control.Right && x >= Control.Left && y <= Control.Bottom && y >= Control.Top;
        }
    }
}