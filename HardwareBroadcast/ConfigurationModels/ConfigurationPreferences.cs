namespace HardwareBroadcast.ConfigurationModels;

public class ConfigurationPreferences
{
    public ApiConfigModel apiConfig { get; set; }
    public KafkaConfigModel kafkaConfig { get; set; }
    
    public static ConfigurationPreferences FactoryCreateDefault()  => new()
    {
        apiConfig = new()
        {
            appUrl = "localhost:7063"
        },
        kafkaConfig = new()
        {
            bootstrapServer = "localhost:9092",
            topic = "hardware_streaming",
            groupId = "hardware_broadcast"
        }
    };
}