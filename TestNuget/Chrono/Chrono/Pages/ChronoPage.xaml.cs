using System;
using static Chrono.ChronoManager;

namespace Chrono
{
    /// <summary>
    /// Chrono entry page.
    /// Chrono page is just interim page to start Chrono Manager UI and hence it
    /// shall not be displayed. Chrono Manager Calendar page shall be started directly.
    /// </summary>
    public partial class ChronoPage : Shell
    {
        private readonly IChronoApplicationObserver clientActionObserver;

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public ChronoPage()
        {
            InitializeComponent();
            clientActionObserver = DependencyService.Get<IChronoApplicationObserver>();
            ChronoManager.Instance.StartChrono(Navigation);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Is called when shell navigation is in progress.
        /// </summary>
        /// <param name="args">Event arguments</param>
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            // Chrono page shall be closed once auditor presses back on Chrono Manager Calendar page
            if (args.Source == ShellNavigationSource.PopToRoot)
            {
                clientActionObserver.OnCloseFormsApplication();
            }
        }
        #endregion
    }
}