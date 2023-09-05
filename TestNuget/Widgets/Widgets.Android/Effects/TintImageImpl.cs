using Android.Graphics;
using Android.Widget;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using Utility = Widgets.Utilities;

[assembly: ExportEffect(typeof(Widgets.Droid.TintImageImpl), nameof(Widgets.TintImage))]
namespace Widgets.Droid
{
    /// <summary>
    /// Tint Image Android implementation
    /// </summary>
    public class TintImageImpl : PlatformEffect
    {
        /// <summary>
        /// Method that is called after the effect is attached and made valid.
        /// </summary>
        protected override void OnAttached()
        {
            try
            {
                var effect = (TintImage)Element.Effects.FirstOrDefault(e => e is TintImage);

                if (effect == null || !(Control is ImageView image))
                    return;

                if (effect.TintColor != Utility.DefaultColor)
                {
                    var filter = new PorterDuffColorFilter(effect.TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                    image.SetColorFilter(filter);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Method that is called after the effect is detached and invalidated.
        /// </summary>
        protected override void OnDetached() { }
    }
}