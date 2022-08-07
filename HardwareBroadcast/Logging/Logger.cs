namespace HardwareBroadcast.Logging;

public class Logger : ILogger
{
    private Serilog.Core.Logger _logger { get; }

    public Logger(Serilog.Core.Logger logger)
    {
        _logger = logger;
    }
    
    public void LogMessage(string message)
    {
        _logger.Information(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warning(message);
    }

    public void LogError(Exception exception)
    {
        _logger.Error(exception, exception.Message);
    }
}