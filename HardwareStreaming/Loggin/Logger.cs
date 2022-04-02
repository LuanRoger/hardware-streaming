using Serilog;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace HardwareStreaming.Loggin;

public class Logger : ILogger
{
    public void InitGlobalLogger()
    {
        var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        Log.Logger = log;
    }
    
    public void LogInformation(string message)
    {
        Log.Logger.Information(message);
    }

    public void LogError(string message)
    {
        Log.Logger.Error(message);
    }

    public void LogWarning(string message)
    {
        Log.Logger.Warning(message);
    }

    public void LogFatal(string message)
    {
        Log.Logger.Fatal(message);
    }
}