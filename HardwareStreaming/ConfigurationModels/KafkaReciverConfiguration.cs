namespace HardwareStreaming.ConfigurationModels;

public class KafkaDomainConfiguration
{
    public string bootstrapServer { get; set; }
    public string topic { get; set; }
    public string clientId { get; set; }
}