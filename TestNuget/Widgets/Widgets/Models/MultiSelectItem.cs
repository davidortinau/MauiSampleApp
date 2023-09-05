namespace Widgets.Models
{
    /// <summary>
    /// Multi Select item
    /// </summary>
    public class MultiSelectItem
    {
        /// <summary>
        /// Gets or sets the item name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets IsSelected flag
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Creates a new object that is a copy of the current instance
        /// </summary>
        /// <returns>A new object that is a copy of this instance</returns>
        public MultiSelectItem Clone()
        {
            return (MultiSelectItem)this.MemberwiseClone();
        }
    }
}
