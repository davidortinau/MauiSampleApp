namespace Widgets.Models
{
    /// <summary>
    /// Sort item info
    /// </summary>
    public class SortItemInfo
    {
        public SortItemType Type { get; set; }
        public string Title { get; set; }
        public int ClickCount { get; set; }
    }

    /// <summary>
    /// Sort item type enumeration
    /// </summary>
    public enum SortItemType
    {
        Ascending,
        Descending
    }
}
