namespace LoggingService
{
    public interface ILoggerManager
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message);

        void LogDebug(string messageTemplate, params object[] propertyValues);
        void LogInformation(string messageTemplate, params object[] propertyValues);
        void LogWarning(string messageTemplate, params object[] propertyValues);
        void LogError(string messageTemplate, params object[] propertyValues);
    }
}
