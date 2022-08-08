namespace HardwareBroadcast.Logging;

public class Logger : ILogger
{
    private Serilog.Core.Logger _logger { get; }

    public Logger(Serilog.Core.Logger logger)
    {
        _logger = logger;
    }
    
    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warning(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }

    public void LogError(Exception exception)
    {
        _logger.Error(exception, exception.Message);
    }

    public void LogFatal(string message)
    {
        _logger.Fatal(message);
    }

    public void LogFatal(Exception exception)
    {
        _logger.Fatal(exception, exception.Message);
    }
}