namespace HardwareBroadcast.ConfigurationModels;

public class KafkaReciverConfiguration
{
    public string bootstrapServer { get; set; }
    public string groupId { get; set; }
    public string topic { get; set; }
}