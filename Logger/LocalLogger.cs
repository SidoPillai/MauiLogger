namespace MauiLogger.Logger
{
    public class LocalLogger : ILogger
    {
        private static readonly string LogFilePath = Path.Combine(FileSystem.AppDataDirectory, "app_log.txt");
        private static readonly object LockObject = new object();

        public LocalLogger()
        {
            InitializeLogFile();
        }

        private void InitializeLogFile()
        {
            lock (LockObject)
            {
                if (File.Exists(LogFilePath))
                {
                    File.WriteAllText(LogFilePath, string.Empty);
                }
                else
                {
                    File.Create(LogFilePath).Dispose();
                }

                LogAppStarted();
                Console.WriteLine($"Log file path: {LogFilePath}");
            }
        }

        public void LogAppStarted()
        {
            Log("App Started");
        }
        public void LogAppActivated()
        {
            Log("App Active");
        }
        public void LogAppDeactivated()
        {
            Log("App InActive");
        }

        public void LogAppExited()
        {
            Log("App Exited");
        }

        public void LogEnterPage(string pageName)
        {
            Log($"Enter Page: {pageName}");
        }

        public void LogExitPage(string pageName)
        {
            Log($"Exit Page: {pageName}");
        }

        public void LogTappedElement(string elementName)
        {
            Log($"Tapped Element: {elementName}");
        }

        public void LogHandledException(Exception ex)
        {
            Log($"Handled Exception: {ex}");
        }

        public void LogRuntimeCrash(Exception ex)
        {
            Log($"Runtime Crash: {ex}");
        }

        private void Log(string message)
        {
            lock (LockObject)
            {
                File.AppendAllText(LogFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
            }
        }
    }
}
