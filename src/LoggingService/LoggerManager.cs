using Serilog;

namespace LoggingService
{
    public class LoggerManager(ILogger logger) : ILoggerManager
    {
        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInformation(string message)
        {
            logger.Information(message);
        }

        public void LogWarning(string message)
        {
            logger.Warning(message);
        }

        public void LogDebug(string messageTemplate, params object[] propertyValues)
        {
            logger.Debug(messageTemplate, propertyValues);
        }

        public void LogInformation(string messageTemplate, params object[] propertyValues)
        {
            logger.Information(messageTemplate, propertyValues);
        }

        public void LogWarning(string messageTemplate, params object[] propertyValues)
        {
            logger.Warning(messageTemplate, propertyValues);
        }

        public void LogError(string messageTemplate, params object[] propertyValues)
        {
            logger.Error(messageTemplate, propertyValues);
        }

    }
}
