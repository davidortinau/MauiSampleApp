using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.Lifecycle;
using Java.Interop;
using Application = Android.App.Application;

namespace Widgets.Droid
{
    /// <summary>
    /// NIQ application class
    /// </summary>
    public abstract class NIQApplicationDroid : MauiApplication, Application.IActivityLifecycleCallbacks, ILifecycleObserver
    {
        #region Constants
        private static readonly string TAG = typeof(NIQApplicationDroid).Name;
        #endregion

        #region Private fields
        private bool isInitialLaunch = false;
        private DateTime appStartTime;

        private int foregroundActivitiesCount = 0;
        #endregion

        #region Public properties
        public bool IsBackground { get; private set; }

        public bool NeedsLock { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NIQApplicationDroid"/> class
        /// </summary>
        /// <param name="javaReference">Java reference.</param>
        /// <param name="transfer">Transfer.</param>
        public NIQApplicationDroid(IntPtr javaReference, JniHandleOwnership transfer)
                : base(javaReference, transfer)
        {
            RegisterActivityLifecycleCallbacks(this);
            ProcessLifecycleOwner.Get().Lifecycle.AddObserver(this);
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Called when the NIQApp is launching
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();

            InitEventHandlerExceptions();
        }

        /// <summary>
        /// Called when the NIQApp is closing
        /// </summary>
        public override void OnTerminate()
        {
            UnregisterActivityLifecycleCallbacks(this);
            base.OnTerminate();
        }

        /// <summary>
        /// Creates Maui application
        /// </summary>
        /// <returns></returns>
        protected override MauiApp CreateMauiApp()
        {
            return BuildMauiApp();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Called when app goes to foreground
        /// </summary>
        [Lifecycle.Event.OnStart]
        [Export]
#pragma warning disable IDE0051 // Remove unused private members
        private void OnForegrounded()
#pragma warning restore IDE0051 // Remove unused private members
        {
            OnBackGroundStateChanged(true);
        }

        /// <summary>
        /// Called when app goes to background
        /// </summary>
        [Lifecycle.Event.OnStop]
        [Export]
#pragma warning disable IDE0051 // Remove unused private members
        private void OnBackgrounded()
#pragma warning restore IDE0051 // Remove unused private members
        {
            OnBackGroundStateChanged(false);
        }

        /// <summary>
        /// Initializes event handler for exceptions
        /// </summary>
        private void InitEventHandlerExceptions()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        }

        /// <summary>
        /// Called when task has exception
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Args</param>
        private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
        }

        /// <summary>
        /// Called when android has exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void HandleAndroidException(object sender, RaiseThrowableEventArgs e)
        {
        }

        /// <summary>
        /// Called when current domain has exception
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Args</param>
        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exc = e.ExceptionObject as Exception;
        }
        #endregion

        #region IActivityLifecycleCallbacks
        /// <summary>
        /// Called when a new activity created event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public virtual void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            isInitialLaunch = foregroundActivitiesCount == 0;
        }

        /// <summary>
        /// Called when an activity destroyed event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        public virtual void OnActivityDestroyed(Activity activity)
        {
            if (foregroundActivitiesCount == 0)
            {
            }
        }

        /// <summary>
        /// Called when the activity paused event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        public virtual void OnActivityPaused(Activity activity)
        {
        }

        /// <summary>
        /// Called when the activity resumed event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        public virtual void OnActivityResumed(Activity activity)
        {
        }

        /// <summary>
        /// Called when the activity save instance state event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        /// <param name="outState">Out state.</param>
        public virtual void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        /// <summary>
        /// Called when a new activity started event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        public virtual void OnActivityStarted(Activity activity)
        {
            foregroundActivitiesCount++;
        }

        /// <summary>
        /// Called when the activity stopped event.
        /// </summary>
        /// <param name="activity">Activity.</param>
        public virtual void OnActivityStopped(Activity activity)
        {
            foregroundActivitiesCount--;
        }

        /// <summary>
        /// Builds Maui Application
        /// </summary>
        /// <returns></returns>
        public abstract MauiApp BuildMauiApp();

        /// <summary>
        /// Called when background state of app was changed
        /// </summary>
        /// <param name="isForeground">Application state.</param>
        private void OnBackGroundStateChanged(bool isForeground)
        {
            if (isForeground)
            {
                if (isInitialLaunch)
                {
                    appStartTime = DateTime.Now;
                    isInitialLaunch = false;
                }

                var appWorkingTime = DateTime.Now - appStartTime;
                IsBackground = false;

                if (NeedsLock)
                {
                    NeedsLock = false;
                }
            }
            else
            {
                IsBackground = true;
            }
        }
        #endregion
    }
}