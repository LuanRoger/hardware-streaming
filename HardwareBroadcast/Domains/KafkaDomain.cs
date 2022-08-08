using Confluent.Kafka;
using HardwareBroadcast.Logging;
using ILogger = HardwareBroadcast.Logging.ILogger;

namespace HardwareBroadcast.Domains;

public class KafkaDomain
{
    private ILogger _logger { get; }
    private ConsumerConfig kafkaConfig { get; }
    
    public KafkaDomain(Logger logger, string bootstrapServer, string groupId)
    {
        _logger = logger;
        kafkaConfig = new()
        {
            BootstrapServers = bootstrapServer,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }
    
    public void StartConsumeLoop(CancellationToken cancellationToken)
    {
        using var consumer = new ConsumerBuilder<string, float>(kafkaConfig).Build();
        consumer.Subscribe("hardware_streaming");

        while (!cancellationToken.IsCancellationRequested)
        {
            var message = consumer.Consume(cancellationToken);
            _logger.LogInformation($"Message consumed: [{message.Message.Key}] - {message.Message.Value}");
        }
        cancellationToken.ThrowIfCancellationRequested();
        
        consumer.Close();
    }
}