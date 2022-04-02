namespace HardwareStreaming.Loggin;

public interface ILogger
{
    void LogInformation(string message);
    void LogError(string message);
    void LogWarning(string message);
    void LogFatal(string message);
}