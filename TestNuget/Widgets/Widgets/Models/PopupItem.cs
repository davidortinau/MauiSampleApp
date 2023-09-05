namespace Widgets.Models
{
    /// <summary>
    /// Popup item
    /// </summary>
    public class PopupItem
    {
        /// <summary>
        /// Gets or sets index of popup item
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Flag, which indicates, if the item is enabled or not
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}
