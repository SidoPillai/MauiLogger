using MauiLogger.Logger;

namespace MauiLogger
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalLogger _logger;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            _logger = ((App)Application.Current).GetLocalLogger();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _logger.LogEnterPage(nameof(MainPage));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _logger.LogExitPage(nameof(MainPage));
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            _logger.LogTappedElement(nameof(CounterBtn));
        }

        private void OnCrashButtonClicked(object sender, EventArgs e)
        {
            try
            {
                // Simulate an app crash by throwing an exception
                throw new Exception("Simulated app crash");
            }
            catch (Exception ex)
            {
                _logger.LogRuntimeCrash(ex);
                throw; // Rethrow the exception to simulate the crash
            }
        }
    }

}
