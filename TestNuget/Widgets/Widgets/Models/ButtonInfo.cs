namespace Widgets.Models
{
    /// <summary>
    /// Title button info
    /// </summary>
    public class DialogTitleButtonInfo
    {
        public ImageSource ImageSource { get; set; }
        public Color Color { get; set; }
        public Action OnClicked { get; set; }
    }

    /// <summary>
    /// Alert button info
    /// </summary>
    public class ButtonInfo
    {
        public string Title { get; set; }
        public Action<View> OnClicked { get; set; }
        public ButtonType ButtonType { get; set; }
        public bool IsEnabled { get; set; } = true;
    }

    /// <summary>
    /// Button type
    /// </summary>
    public enum ButtonType
    {
        Neutral,
        Negative,
        Positive
    }
}
