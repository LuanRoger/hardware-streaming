namespace HardwareStreaming.Internals.Loggin.Providers;

public interface ILoggerProvider
{
    void Information(string message);
    void Warning(string message);
    void Error(string message);
    void Error(Exception exception);
    void Fatal(string message);
    void Fatal(Exception exception);
}