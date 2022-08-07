namespace HardwareBroadcast.Logging;

public interface ILogger
{
    public void LogMessage(string message);
    public void LogWarning(string message);
    public void LogError(Exception exception);
}