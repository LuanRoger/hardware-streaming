using HardwareStreaming.Internals.Configuration.ConfigsFormaters;

namespace HardwareStreaming.Internals.Configuration;

public class ConfigurationManager<T>
{
    private string filePath { get; }
    private ConfigFormater<T> _configFormater { get; }

    public ConfigurationManager(ConfigFormater<T> configFormater, string filePath)
    {
        _configFormater = configFormater;
        this.filePath = filePath;
    }
    
    public void CreateDefaultConfiguration(T defaultValue) => 
        _configFormater.SaveDefaultConfig(filePath, defaultValue);
    public T? LoadConfiguration() => 
        _configFormater.LoadConfiguration(filePath);
}