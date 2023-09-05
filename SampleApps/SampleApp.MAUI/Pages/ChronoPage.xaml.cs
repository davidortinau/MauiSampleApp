using System.Windows.Input;
using Chrono;

namespace SampleApp.MAUI
{
    public partial class ChronoPage : ContentPage
    {
        public ICommand StartChronoCommand { get; }
        public ICommand ShowLongInactivyDialogCommand { get; }

        public ChronoPage()
        {
            InitializeComponent();
            StartChronoCommand = new Command(StartChrono);
            ShowLongInactivyDialogCommand = new Command(ShowLongInactivyDialog);

            BindingContext = this;
        }

        private void StartChrono()
        {
            ChronoManager.Instance.StartChrono(Navigation);
        }

        private void ShowLongInactivyDialog()
        {
            Action<ChronoManager.ChronoNewActivityActionType> selectedAction = (action) =>
            {
            };
            ChronoManager.Instance.ShowLongInactivyDialog(null, DateTime.Now, selectedAction);
        }
    }
}