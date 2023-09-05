using Android.Content;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using System.ComponentModel;

namespace Widgets.Droid
{
    /// <summary>
    /// NIQ Carousel View Renderer
    /// </summary>
    public class NIQCarouselRenderer : CarouselViewRenderer
    {
        private bool isAllowedToChangePosition = false;

        /// <summary>
        /// Initializes the instance of <see cref="NIQCarouselRenderer.cs"/>
        /// </summary>
        /// <param name="context">Context</param>s
        public NIQCarouselRenderer(Context context) : base(context)
        {
        }

        #region Overridables
        /// <summary>
        /// Raises On Element Property Changed Event
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="changedProperty">Changed property</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs changedProperty)
        {
            if (changedProperty.PropertyName == "IsDragging")
            {
                isAllowedToChangePosition = true;
            }
            if (changedProperty.PropertyName == "Position")
            {
                if (Carousel.Position != (Carousel as NIQCarouselView).ManualSetPosition && !isAllowedToChangePosition)
                {
                    Carousel.Position = (Carousel as NIQCarouselView).ManualSetPosition;
                }
                else
                {
                    isAllowedToChangePosition = false;
                }
            }

            base.OnElementPropertyChanged(sender, changedProperty);
        }
        #endregion
    }
}