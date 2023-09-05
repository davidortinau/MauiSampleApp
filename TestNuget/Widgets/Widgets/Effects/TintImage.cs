namespace Widgets
{
    /// <summary>
    /// Tint Image effect
    /// </summary>
    public static class TintImageEffect
    {
        /// <summary>
        /// Tint color property
        /// </summary>
        public static readonly BindableProperty TintColorProperty = BindableProperty.CreateAttached(
            "TintColor", typeof(Color), typeof(TintImageEffect), Utilities.DefaultColor,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                if (!(bindable is View view))
                {
                    return;
                }

                Color color = (Color)newValue;
                if (color == Utilities.DefaultColor)
                {
                    var toRemove = view.Effects.FirstOrDefault(e => e is TintImage);
                    if (toRemove != null)
                    {
                        view.Effects.Remove(toRemove);
                    }
                }
                else
                {
                    view.Effects.Clear();
                    view.Effects.Add(new TintImage(color));
                }
            });

        /// <summary>
        /// Gets tint color
        /// </summary>
        /// <param name="view">View</param>
        /// <returns>Tint color</returns>
        public static Color GetTintColor(BindableObject view) =>
            (Color)view.GetValue(TintColorProperty);

        /// <summary>
        /// Sets tint color
        /// </summary>
        /// <param name="view">View</param>
        /// <param name="value">Value to be set</param>
        public static void SetTintColor(BindableObject view, Color value) =>
            view.SetValue(TintColorProperty, value);
    }

    /// <summary>
    /// Tint Image
    /// </summary>
    public class TintImage : RoutingEffect
    {
        /// <summary>
        /// Tint color
        /// </summary>
        public Color TintColor { get; set; }

        /// <summary>
        /// Creates new instance of <see cref="TintImage"/> class
        /// </summary>
        /// <param name="color">Tint color</param>
        public TintImage(Color color) : base($"{nameof(TintImage)}")
        {
            TintColor = color;
        }
    }
}
