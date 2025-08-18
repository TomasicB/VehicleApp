namespace Vehicle.Common;

public class Logger : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel <= LogLevel.Information;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}]: {formatter(state, exception)}";

        if (exception != null)
        {
            logMessage += Environment.NewLine + exception;
        }

        File.AppendAllText(AppContext.BaseDirectory, logMessage + Environment.NewLine);
    }
}
