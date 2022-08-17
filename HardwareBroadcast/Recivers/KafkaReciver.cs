using Confluent.Kafka;
using HardwareStreaming.Internals.Loggin;

namespace HardwareBroadcast.Recivers;

public class KafkaReciver
{
    private Logger _logger { get; }
    private ConsumerConfig kafkaConfig { get; }
    private string topic { get; }
    
    public KafkaReciver(Logger logger,
        string bootstrapServer, string topic, string groupId)
    {
        _logger = logger;
        kafkaConfig = new()
        {
            BootstrapServers = bootstrapServer,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        this.topic = topic;
    }
    
    public void StartConsumeLoop(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<string, float>(kafkaConfig).Build();
        consumer.Subscribe(topic);

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = consumer.Consume(cancellationToken);
            
            string key = message.Message.Key;
            float messageValue = message.Message.Value;
            
            _logger.LogInformation($"Message consumed: [{key}] - {messageValue}");
        }
        cancellationToken.ThrowIfCancellationRequested();
        
        consumer.Close();
    }
}