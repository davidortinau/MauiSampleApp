using System.Windows.Input;

namespace Widgets
{
    public partial class NIQPicker : ContentView
    {
        #region Events
        public delegate void PickerValueChangedEventHandler(View sender);
        public event PickerValueChangedEventHandler ValueChanged;
        #endregion

        #region Private members
        private readonly List<PickerItem> selectedItems = new List<PickerItem>();
        #endregion

        #region Bindable properties
        /// <summary>
        /// Items list bindable property
        /// </summary>
        private static readonly BindableProperty ItemsListProperty =
            BindableProperty.Create(nameof(ItemsList), typeof(List<PickerItem>), typeof(NIQPicker),
                defaultValue: null, propertyChanged: (sender, oldV, newV) =>
                {
                    if (oldV != newV)
                    {
                        var picker = sender as NIQPicker;
                        picker.SelectedItems.Clear();

                        var newItems = (List<PickerItem>)newV;
                        IEnumerable<PickerItem> newSelectedItems = newItems.Where(item => item.IsSelected);
                        picker.SelectedItems.AddRange(newSelectedItems);
                    }
                });

        /// <summary>
        /// Picker mode bindable property
        /// </summary>
        private static readonly BindableProperty PickerModeProperty =
            BindableProperty.Create(nameof(Mode), typeof(PickerMode), typeof(NIQPicker),
                defaultValue: PickerMode.SingleChoice);

        /// <summary>
        /// Is error flag bindable property
        /// </summary>
        private static readonly BindableProperty IsErrorProperty =
            BindableProperty.Create(nameof(IsError), typeof(bool), typeof(NIQPicker), defaultValue: false);
        #endregion

        #region Properties
        /// <summary>
        /// Gets items number in row
        /// </summary>
        public int ItemsNumberInRow => GetItemsNumberInRow();

        /// <summary>
        /// Gets or sets is error flag
        /// </summary>
        public bool IsError
        {
            get => (bool)GetValue(IsErrorProperty);
            set => SetValue(IsErrorProperty, value);
        }

        /// <summary>
        /// Gets or sets picker mode
        /// </summary>
        public PickerMode Mode
        {
            get => (PickerMode)GetValue(PickerModeProperty);
            set => SetValue(PickerModeProperty, value);
        }

        /// <summary>
        /// Gets or sets picker items list
        /// </summary>
        public List<PickerItem> ItemsList
        {
            get => (List<PickerItem>)GetValue(ItemsListProperty);
            set => SetValue(ItemsListProperty, value);
        }

        /// <summary>
        /// Gets or sets button clicked command
        /// </summary>
        public ICommand ButtonClickedCommand { get; set; }

        /// <summary>
        /// Gets the selected items list
        /// </summary>
        public List<PickerItem> SelectedItems => selectedItems;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="NIQPicker"/>
        /// </summary>
        public NIQPicker()
        {
            InitializeComponent();

            ButtonClickedCommand = new Command((parameter) => OnButtonClicked((int)parameter));

            BindingContext = this;
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Handles on button clicked event
        /// </summary>
        /// <param name="index">Index</param>
        private void OnButtonClicked(int index)
        {
            PickerItem clickedItem = ItemsList[index];
            string oldValue = string.Join(",", SelectedItems.Select(item => item.Index));

            if (SelectedItems.Contains(clickedItem))
            {
                clickedItem.IsSelected = false;
                SelectedItems.Remove(clickedItem);
            }
            else
            {
                if (Mode == PickerMode.SingleChoice)
                {
                    SelectedItems.ForEach(item =>
                    {
                        item.IsSelected = false;
                    });
                    SelectedItems.Clear();
                }
                clickedItem.IsSelected = true;
                SelectedItems.Add(clickedItem);
            }

            ValueChanged?.Invoke(this);
        }

        private int GetItemsNumberInRow()
        {
            int itemsNumber = 0;
            if (ItemsList?.Any() ?? false)
            {
                int itemsCount = ItemsList.Count;
                if (itemsCount == 1)
                {
                    itemsNumber = 1;
                }
                else if (itemsCount == 2 || itemsCount == 4)
                {
                    itemsNumber = 2;
                }
                else
                {
                    itemsNumber = 3;
                }
            }
            return itemsNumber;
        }
        #endregion
    }
}