using Android.Content;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;

namespace Widgets.Droid
{
    /// <summary>
    /// NIQ Scroll View Renderer
    /// </summary>
    public class NIQScrollViewRenderer : ScrollViewRenderer
    {
        /// <summary>
        /// Initializes the instance of <see cref="NIQScrollViewRenderer.cs"/>
        /// </summary>
        /// <param name="context">Context</param>
        public NIQScrollViewRenderer(Context context) : base(context)
        {
        }

        /// <summary>
        /// Raises On Element Changed Event
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if ((e.NewElement as ScrollView).VerticalScrollBarVisibility == ScrollBarVisibility.Always)
            {
                ScrollbarFadingEnabled = false;
            }
        }
    }
}