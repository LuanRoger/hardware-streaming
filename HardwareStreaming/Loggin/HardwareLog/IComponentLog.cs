namespace HardwareStreaming.Loggin.HardwareLog;

public interface IComponentLog
{
    void Log(ILogger logger, in Computer computer, out Dictionary<string, double> logedInfos);
}