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

    #region ConfigFileStreamingUtils
    private interface IFileModeStreamer
    {
        string Stream(string filePath);
    }
    private class CreateReadDefault : IFileModeStreamer
    {
        public string Stream(string filePath)
        {
            File.WriteAllText(filePath, DEFAULT_YAML_CONFIG_FILE_TEMPLATE);
            return DEFAULT_YAML_CONFIG_FILE_TEMPLATE;
        }
    }
    private class ReadFile : IFileModeStreamer
    {
        public string Stream(string filePath) => File.ReadAllText(filePath);
    }
    #endregion

    /// <summary>
    /// Load the configuration file.
    /// If the file not exist, will return false.
    /// If the file is loaded successfully, will return true.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static YamlConfigurationFile? LoadConfigFile(string filePath, ILogger logger, bool create = false)
    {
        bool fileExist = File.Exists(filePath);
        if(!fileExist && !create)
        {
            logger.LogError($"The configuration file not exists in {filePath}");
            return null;
        }
        
        string yamlText = string.Empty;
        if(create)
        {
            logger.LogInformation($"Creating a new configuration file in {filePath}...");
            if(fileExist)
                logger.LogInformation("The specified file already exists, so it will be overwritten.");
            yamlText = new CreateReadDefault().Stream(filePath);
        }
        else
        {
            logger.LogInformation($"Loading the configuration file in {filePath}...");
            yamlText = new ReadFile().Stream(filePath);
        }

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
}