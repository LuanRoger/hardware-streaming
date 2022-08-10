namespace HardwareStreaming.Internals.Configuration.ConfigsFormaters.Yaml;

public class YamlConfigFormater<T> : ConfigFormater<T>
{
    public YamlConfigFormater() : 
        base(new YamlConfigurationLoader<T>(),
            new YamlConfigurationSaver<T>()) { /*Nothing*/ }
}