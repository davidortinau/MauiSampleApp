using System.ComponentModel;

namespace Widgets
{
    public class NIQAbstractViewModel : INotifyPropertyChanged
    {
        #region Private members
        private bool isBusy = false;

        private string title = string.Empty;
        #endregion

        #region Public properties
        /// <summary>
        /// Indicates if view model is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        /// <summary>
        /// Title of view model.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        #endregion

        /// <summary>
        /// Sets property
        /// </summary>
        /// <param name="backingStore">Backing store.</param>
        /// <param name="value">Value</param>
        /// <param name="propertyName">Property name.</param>
        /// <param name="onChanged">On changed action.</param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore, T value,
            string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Executes when propery was changed.
        /// </summary>
        /// <param name="propertyName">Changed property name.</param>
        protected void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
