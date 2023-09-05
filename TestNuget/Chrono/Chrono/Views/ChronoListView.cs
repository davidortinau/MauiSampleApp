
namespace Chrono
{
    /// <summary>
    /// Chrono List View
    /// </summary>
    public class ChronoListView : ListView
    {
        public static readonly BindableProperty ItemsProperty =
          BindableProperty.Create("Items", typeof(List<TimeInterval>), typeof(ChronoListView), new List<TimeInterval>());

        public List<TimeInterval> Items
        {
            get { return (List<TimeInterval>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;
        public event EventHandler<SelectedItemChangedEventArgs> ItemLongClick;

        #region Public methods
        /// <summary>
        /// Notifies about item selection
        /// </summary>
        /// <param name="item">Selected item</param>
        public void NotifyItemSelected(object item, int index)
        {
            if (ItemSelected != null)
            {
                ItemSelected(this, new SelectedItemChangedEventArgs(item, index));
            }
        }

        /// <summary>
        /// Notifies about long click on the item
        /// </summary>
        /// <param name="item">Long clicked item</param>
        public void NotifyItemLongClick(object item, int index)
        {
            if (ItemLongClick != null)
            {
                ItemLongClick(this, new SelectedItemChangedEventArgs(item, index));
            }
        }

        /// <summary>
        /// Resets selection on the items
        /// </summary>
        public void ResetSelection()
        {
            foreach (var item in Items)
            {
                item.IsSelected = false;
            }
            this.OnPropertyChanged(nameof(Items));
        }
        #endregion
    }
}
