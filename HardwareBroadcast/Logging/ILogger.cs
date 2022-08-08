namespace HardwareBroadcast.Logging;

public interface ILogger
{
    public void LogInformation(string message);
    public void LogWarning(string message);
    public void LogError(string message);
    public void LogError(Exception exception);
    public void LogFatal(string message);
    public void LogFatal(Exception exception);
}