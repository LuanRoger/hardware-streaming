namespace HardwareBroadcast.Configuration.Models;

public class KafkaConfigModel
{
    public string bootstrapServer { get; set; }
    public string groupId { get; set; }
    public string topic { get; set; }
}