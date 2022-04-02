using System.Net;
using HardwareStreaming.Configuration.Models;
using HardwareStreaming.Enums;
using HardwareStreaming.Loggin;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace HardwareStreaming.Configuration;

public static class YamlConfigurationManager
{
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
}