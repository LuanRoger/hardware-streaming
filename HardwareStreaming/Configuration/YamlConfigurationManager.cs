using System.Text;
using HardwareStreaming.Configuration.Models;
using HardwareStreaming.Loggin;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HardwareStreaming.Configuration;

public static class YamlConfigurationManager
{
    private const string DEFAULT_YAML_CONFIG_FILE_TEMPLATE = 
        "kafka_domain_configuration:\n" +
        "  bootstrap_server: localhost:9092\n" +
        "  client_id: HardwareProducer1\n" +
        "  topic: hardware_streaming\n" +
        "hardware_monitoring:\n" +
        "  - Cpu\n" +
        "  - Gpu\n" +
        "  - Mainboard\n" +
        "  - Network\n" +
        "  - Ram\n" +
        "delay_stream_time: 300";
    
    /// <summary>
    /// Load the configuration file.
    /// If the file not exist, will return false.
    /// If the file is loaded successfully, will return true.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static YamlConfigurationFile? LoadFromFile(string filePath, ILogger logger)
    {
        if(!File.Exists(filePath))
        {
            logger.LogError($"The configuration file not exists in {filePath}");
            return null;
        }
        
        string yamlText = File.ReadAllText(filePath);
        IDeserializer deserialized = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();
        
        YamlConfigurationFile configurationFile;
        try
        {
            configurationFile = deserialized.Deserialize<YamlConfigurationFile>(yamlText);
        }
        catch(Exception e) 
        { 
            logger.LogError("Occurs a error when try to deserialize the" +
                            $" configuration file: {e.Message}");
            return null;
        }
        
        return configurationFile;
    }
    public static YamlConfigurationFile? CreateDefault(string filePath, ILogger logger)
    {
        logger.LogInformation($"Creating a new configuration file in {filePath}...");
        if(File.Exists(filePath))
                logger.LogWarning("A file already exists in the specified path, so it will be overwritten.");
        
        using (FileStream yamlWriter = new(filePath, FileMode.Create, FileAccess.Write))
        {
            using StreamWriter streamWriter = new(yamlWriter, Encoding.UTF8, DEFAULT_YAML_CONFIG_FILE_TEMPLATE.Length);
            streamWriter.WriteLine(DEFAULT_YAML_CONFIG_FILE_TEMPLATE);
            
            logger.LogInformation($"A new configuration file has been created on {filePath}.");
        }

        return LoadFromFile(filePath, logger);
    }
}