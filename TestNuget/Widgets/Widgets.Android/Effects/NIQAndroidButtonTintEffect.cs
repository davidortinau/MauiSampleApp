using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using AndroidX.AppCompat.Widget;
using Widgets;
using Widgets.Droid;
using System.ComponentModel;

[assembly: ExportEffect(typeof(NIQAndroidButtonTintEffect), nameof(NIQButtonTintEffect))]
namespace Widgets.Droid
{
    /// <summary>
    /// Radio button tint effect class
    /// </summary>
    public class NIQAndroidButtonTintEffect : Microsoft.Maui.Controls.Platform.PlatformEffect
    {
        #region Overridables
        /// <summary>
        /// Called when element is attached
        /// </summary>
        protected override void OnAttached()
        {
            UpdateButtonTint();
        }

        /// <summary>
        /// Called when element is detached
        /// </summary>
        protected override void OnDetached()
        {
        }

        /// <summary>
        /// Called when property is changed
        /// </summary>
        /// <param name="args">Args</param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName.Equals("ButtonTint"))
            {
                UpdateButtonTint();
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Updates button tint
        /// </summary>
        private void UpdateButtonTint()
        {
            var element = Control as AppCompatRadioButton;
            if (element != null)
            {
                var primaryColorList = Utilities.ParseColor(NIQThemeController.Theme.ControlColor);
                var textColorList = Utilities.ParseColor(NIQThemeController.Theme.OnBackground);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                {
                    element.SupportButtonTintList = ColorStateList.ValueOf(primaryColorList);
                }
                else
                {
                    element.Background.SetColorFilter(primaryColorList, PorterDuff.Mode.SrcOut);
                }
                element.SetTextColor(textColorList);
            }
        }
        #endregion
    }
}