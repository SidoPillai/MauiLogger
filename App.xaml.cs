using MauiLogger.Logger;

namespace MauiLogger
{
    public partial class App : Application
    {
        private readonly LocalLogger _localLogger;

        public App()
        {
            InitializeComponent();

            _localLogger = new LocalLogger();

            // Handle unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;            

            Application.Current.Dispatcher.Dispatch(() =>
            {
                var window = Application.Current.Windows.FirstOrDefault();
                if (window != null)
                {
                    window.Activated += (s, e) =>
                    {                        
                        _localLogger.LogAppActivated();
                    };

                    window.Deactivated += (s, e) =>
                    {
                        _localLogger.LogAppDeactivated();
                    };
                    window.Destroying += (s, e) =>
                    {
                        _localLogger.LogAppExited();
                    };                    
                }
            });
        }

        public LocalLogger GetLocalLogger()
        {
            return _localLogger;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                _localLogger.LogRuntimeCrash(ex);
            }
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _localLogger.LogRuntimeCrash(e.Exception);
            e.SetObserved();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}