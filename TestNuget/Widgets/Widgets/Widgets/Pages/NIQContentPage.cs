using System.Diagnostics;

namespace Widgets
{
    public class NIQContentPage : ContentPage
    {
        private const int AntiDoubleClickTimeout = 500;

        /// <summary>
        /// Is Busy property
        /// </summary>
        public static new readonly BindableProperty IsBusyProperty = BindableProperty.Create(
            nameof(IsBusy), typeof(bool), typeof(NIQContentPage));

        /// <summary>
        /// Indicates if page is busy so UI should be blocked
        /// </summary>
        public new bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        /// <summary>
        /// Notify page about control activated
        /// </summary>
        public void NotifyControlActivated()
        {
            IsBusy = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(AntiDoubleClickTimeout), () =>
            {
                IsBusy = false;
                return false;
            });
        }

        protected override void OnAppearing()
        {
            Debug.WriteLine("bababoom");
            base.OnAppearing();
        }
    }
}
