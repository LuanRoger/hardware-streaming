using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HardwareStreaming.Internals.Configuration.ConfigsFormaters.Yaml;

internal class YamlConfigurationLoader<T> : IConfigLoader<T>
{
    private INamingConvention _namingConvention => UnderscoredNamingConvention.Instance;

    /// <summary>
    /// Load the configuration file.
    /// If the file not exist, will return false.
    /// If the file is loaded successfully, will return true.
    /// </summary>
    /// <param name="filePath">Path to configFile</param>
    /// <returns>Return the loaded object if has been loaded sucessfuly, otherwise return null</returns>
    public T? LoadConfigFile(string filePath)
    {
        bool fileExist = File.Exists(filePath);
        if(!fileExist)
            return default;

        string yamlText = File.ReadAllText(filePath);

        IDeserializer deserialized = new DeserializerBuilder()
            .WithNamingConvention(_namingConvention)
            .Build();


        return deserialized.Deserialize<T>(yamlText);;
    }
}