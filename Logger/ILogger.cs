namespace MauiLogger.Logger
{
    internal interface ILogger
    {
        void LogEnterPage(string pageName);
        void LogExitPage(string pageName);
        void LogTappedElement(string elementName);
        void LogHandledException(Exception ex);
        void LogRuntimeCrash(Exception ex);
    }
}
