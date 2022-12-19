using Serilog;

namespace HardwareStreaming.Internals.Loggin.Providers;

public class SerilogLoggerProvider : ILoggerProvider
{
    private Serilog.Core.Logger _logger { get; }
    
    public SerilogLoggerProvider()
    {
        _logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
    }
    public SerilogLoggerProvider(Serilog.Core.Logger logger)
    {
        _logger = logger;
    }

    public void Information(string message)
    {
        _logger.Information(message);
    }

    public void Warning(string message)
    {
        _logger.Warning(message);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Error(Exception exception)
    {
        _logger.Error(exception, exception.Message);
    }

    public void Fatal(string message)
    {
        _logger.Fatal(message);
    }

    public void Fatal(Exception exception)
    {
        _logger.Fatal(exception, exception.Message);
    }
}