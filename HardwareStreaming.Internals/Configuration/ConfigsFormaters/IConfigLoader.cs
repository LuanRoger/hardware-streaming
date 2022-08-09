namespace HardwareStreaming.Internals.Configuration.ConfigsFormaters;

public interface IConfigLoader<out T>
{
    public T? LoadConfigFile(string filePath);
}