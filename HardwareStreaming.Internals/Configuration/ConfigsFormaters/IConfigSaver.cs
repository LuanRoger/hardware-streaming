namespace HardwareStreaming.Internals.Configuration.ConfigsFormaters;

public interface IConfigSaver<T>
{
    public void SaveConfigFile(string filePath, T defaultValue);
}