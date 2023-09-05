namespace Widgets
{
    /// <summary>
    /// NIQ Carousel View
    /// </summary>
    public class NIQCarouselView : CarouselView
    {
        /// <summary>
        /// Manual Set Position property
        /// </summary>
        public int ManualSetPosition
        {
            get { return (int)GetValue(ManualSetPositionProperty); }
            set { SetValue(ManualSetPositionProperty, value); }
        }

        /// <summary>
        /// Manual Set Position bindable property
        /// </summary>
        public static readonly BindableProperty ManualSetPositionProperty =
            BindableProperty.Create(nameof(ManualSetPosition), typeof(int), typeof(NIQCarouselView));
    }
}
