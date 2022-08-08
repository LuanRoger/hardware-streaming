using HardwareBroadcast.Configuration.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ILogger = HardwareBroadcast.Logging.ILogger;

namespace HardwareBroadcast.Configuration;

public class ConfigurationLoader
{
    private ILogger _logger { get; }

    public ConfigurationLoader(ILogger logger)
    {
        _logger = logger;
    }
    
    public YamlConfiguration? LoadConfig(string path)
    {
        bool fileExists = File.Exists(path);
        if(!fileExists)
        {
            _logger.LogError($"The configuration file not exists in {path}");
            return null;
        }
        
        _logger.LogInformation($"Loading the configuration file in {path}...");
        string yamlText = File.ReadAllText(path);
        
        IDeserializer deserialized = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        YamlConfiguration configuration;
        try
        {
            configuration = deserialized.Deserialize<YamlConfiguration>(yamlText);
        }
        catch(Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
        
        return configuration;
    }
    
    public bool SaveDefaultConfig(string path)
    {
        ISerializer serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        
        YamlConfiguration defaultConfig = new()
        {
            apiConfig = new()
            {
                server = "localhost",
                port = "9092"
            },
            kafkaConfig = new()
            {
                bootstrapServer = "localhost:9092",
                groupId = "hardware_bradcast",
                topic = "hardware_streaming"
            }
        };
        
        string yamlText = serializer.Serialize(defaultConfig);
        
        File.WriteAllText(path, yamlText);

        return true;
    }
}