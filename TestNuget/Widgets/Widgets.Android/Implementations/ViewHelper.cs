using Android.Util;
using Android.Content.Res;
using Microsoft.Maui.Platform;

namespace Widgets.Droid
{
    public class ViewHelper: IViewHelper
    {
        /// <summary>
        /// Gets view position rectangle
        /// </summary>
        /// <param name="view">View</param>
        /// <returns>Position rectangle</returns>
        public Rect GetViewGlobalRectangle(View view)
        {
            int statusBarHeight = 0;
            Resources resources = MauiApplication.Context.Resources;
            int resourceId = resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = resources.GetDimensionPixelSize(resourceId);
            }

            var mauiContext = view.Handler.MauiContext;
            var nativeView = ElementExtensions.ToPlatform(view, mauiContext);
            var viewRectangle = new Android.Graphics.Rect();
            if (nativeView != null && nativeView.GetGlobalVisibleRect(viewRectangle))
            {
                DisplayMetrics metrics = resources.DisplayMetrics;
                return new Rect(
                    viewRectangle.Left / metrics.Density,
                    (viewRectangle.Top - statusBarHeight) / metrics.Density,
                    viewRectangle.Width() / metrics.Density,
                    viewRectangle.Height() / metrics.Density);
            }

            return new Rect();
        }
    }
}
