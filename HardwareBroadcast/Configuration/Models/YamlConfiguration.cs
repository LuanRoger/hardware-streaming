namespace HardwareBroadcast.Configuration.Models;

public class YamlConfiguration
{
    public ApiConfigModel apiConfig { get; set; }
    public KafkaConfigModel kafkaConfig { get; set; }
}