using HardwareStreaming.Internals.Loggin.Providers;

namespace HardwareStreaming.Internals.Loggin;

public class Logger : ILogger
{
    private ILoggerProvider _logger { get; }

    public Logger(ILoggerProvider logger)
    {
        _logger = logger;
    }
    public Logger(Serilog.Core.Logger logger)
    {
        _logger = new SerilogLoggerProvider(logger);
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