namespace HardwareStreaming.Internals.Loggin;

public interface ILogger
{
    public void LogInformation(string message);
    public void LogError(string message);
    public void LogError(Exception exception);
    public void LogWarning(string message);
    public void LogFatal(string message);
    public void LogFatal(Exception exception);
}