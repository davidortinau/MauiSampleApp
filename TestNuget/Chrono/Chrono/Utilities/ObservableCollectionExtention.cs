using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Chrono
{
    /// <summary>
    /// Extention for Observable Collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionExtention<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Initializes the instance of <see cref="ObservableCollectionExtention.cs"/>
        /// </summary>
        /// <param name="collection">Collection</param>
        public ObservableCollectionExtention(List<T> collection) : base(collection)
        {
        }

        /// <summary>
        /// Inserts several items
        /// </summary>
        /// <param name="newItems">Items to be inserted</param>
        /// <param name="index">Start index</param>
        public void InsertItems(List<T> newItems, int index)
        {
            if (newItems != null && newItems.Count > 1)
            {
                for (int i = 0; i < newItems.Count; i++)
                {
                    Items.Insert(index + i, newItems[i]);
                }
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, index));
            }
            else if (newItems.Count == 1)
            {
                base.Insert(0, newItems[0]);
            }
        }

        /// <summary>
        /// Adds several items to the end
        /// </summary>
        /// <param name="newItems">Items to be added</param>
        public void AddItems(List<T> newItems)
        {
            if (newItems != null && newItems.Count > 1)
            {
                var startIndex = Items.Count;
                foreach (var item in newItems)
                {
                    Items.Add(item);
                }
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, startIndex));
            }
            else if (newItems.Count == 1)
            {
                base.Add(newItems[0]);
            }
        }

        /// <summary>
        /// Removes items
        /// </summary>
        /// <param name="fromIndex">From index</param>
        /// <param name="count">Count</param>
        public void RemoveItems(int fromIndex, int count)
        {
            if (count > 1)
            {
                var itemsToRemove = new List<T>();
                for (int i = 0; i < count; i++)
                {
                    var itemToRemove = Items[fromIndex];
                    itemsToRemove.Add(itemToRemove);
                    Items.Remove(itemToRemove);
                }
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, itemsToRemove, fromIndex));
            }
            else if (count == 1)
            {
                var itemToRemove = Items[fromIndex];
                base.Remove(itemToRemove);
            }
        }

        /// <summary>
        /// Replaces items
        /// </summary>
        /// <param name="itemsToBeReplaced">Items to be replaced</param>
        /// <param name="itemsToReplace">Items for replacement</param>
        public void Replace(List<T> itemsToBeReplaced, List<T> itemsToReplace)
        {
            var indexOfReplace = itemsToBeReplaced.Count != 0 ? Items.IndexOf(itemsToBeReplaced[0]) : 0;
            foreach(var item in itemsToBeReplaced)
            {
                var indexOfRemove = Items.IndexOf(item);
                Items.Remove(item);
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, indexOfRemove));
            }
            if (indexOfReplace != 0)
            {
                AddItems(itemsToReplace);
            }
            else
            {
                InsertItems(itemsToReplace, 0);
            }
        }
    }
}
