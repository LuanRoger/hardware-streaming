using HardwareStreaming.Internals.Loggin.LogginCore;

namespace HardwareStreaming.Internals.Loggin;

public class Logger
{
    private ILoggerCore _logger { get; }

    public Logger(ILoggerCore logger)
    {
        _logger = logger;
    }
    
    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogError(string message)
    {
        _logger.Error(message);
    }

    public void LogError(Exception exception)
    {
        _logger.Error(exception);
    }

    public void LogWarning(string message)
    {
        _logger.Warning(message);
    }

    public void LogFatal(string message)
    {
        _logger.Fatal(message);
    }

    public void LogFatal(Exception exception)
    {
        _logger.Fatal(exception);
    }
}