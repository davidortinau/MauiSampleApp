using System.ComponentModel;

namespace Widgets
{
    /// <summary>
    /// Picker item
    /// </summary>
    public class PickerItem : INotifyPropertyChanged
    {
        private bool isSelected;

        public int Index { get; set; }
        public string Title { get; set; }
        public ImageSource ItemIcon { get; set; }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }

    /// <summary>
    /// Picker mode enumeration
    /// </summary>
    public enum PickerMode
    {
        SingleChoice,
        MultiChoice
    }
}
