using Confluent.Kafka;
using HardwareBroadcast.Logging;

namespace HardwareBroadcast.Domains;

public class KafkaDomain
{
    private Logger _logger { get; }
    private ConsumerConfig kafkaConfig { get; } = new()
    {
        BootstrapServers = "localhost:9092",
        GroupId = "hardware-broadcast",
        AutoOffsetReset = AutoOffsetReset.Earliest
    };
    
    public KafkaDomain(Logger logger)
    {
        _logger = logger;
    }
    
    public void StartConsumeLoop(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<string, float>(kafkaConfig).Build();
        consumer.Subscribe("hardware_streaming");

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = consumer.Consume(cancellationToken);
            _logger.LogMessage($"Message consumed: [{message.Message.Key}] - {message.Message.Value}");
        }
        cancellationToken.ThrowIfCancellationRequested();
        
        consumer.Close();
    }
}