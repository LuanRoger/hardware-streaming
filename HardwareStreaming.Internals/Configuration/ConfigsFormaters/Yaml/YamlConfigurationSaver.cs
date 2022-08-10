using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HardwareStreaming.Internals.Configuration.ConfigsFormaters.Yaml;

internal class YamlConfigurationSaver<T> : IConfigSaver<T>
{
    private INamingConvention _namingConvention => UnderscoredNamingConvention.Instance;

    public void SaveConfigFile(string filePath, T defaultValue)
    {
        if(File.Exists(filePath) || defaultValue == null)
            return;

        ISerializer serializer = new SerializerBuilder()
            .WithNamingConvention(_namingConvention)
            .Build();
        
        string yamlObject = serializer.Serialize(defaultValue);
        File.WriteAllText(filePath, yamlObject);
    }
}