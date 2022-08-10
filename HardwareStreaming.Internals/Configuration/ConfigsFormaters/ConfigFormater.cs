namespace HardwareStreaming.Internals.Configuration.ConfigsFormaters;

public abstract class ConfigFormater<T>
{
    protected IConfigLoader<T> configLoader { get; }
    protected IConfigSaver<T> configSaver { get; }
    
    protected ConfigFormater(IConfigLoader<T> configLoader, IConfigSaver<T> configSaver)
    {
        this.configLoader = configLoader;
        this.configSaver = configSaver;
    }
    
    internal void SaveDefaultConfig(string filePath, T defaultValue) => 
        configSaver.SaveConfigFile(filePath, defaultValue);
    internal T? LoadConfiguration(string filePath) => 
        configLoader.LoadConfigFile(filePath);
}