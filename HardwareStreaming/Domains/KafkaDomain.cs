using System.Globalization;
using Confluent.Kafka;
using HardwareStreaming.Loggin;

namespace HardwareStreaming.Domains;

public class KafkaDomain : IDomain
{
    public ProducerConfig producerConfig { get; }
    private ILogger _logger { get; }
    private string kafkaTopic { get; }

    public KafkaDomain(ProducerConfig producerConfig, ILogger logger, string kafkaTopicName)
    {
        this.producerConfig = producerConfig;
        _logger = logger;
        kafkaTopic = kafkaTopicName;
    }

    public void StreamInfo(KeyValuePair<string, double> idValuePair, ILogger logger, int flushTimeout)
    {
        using var producer = new ProducerBuilder<string, double>(producerConfig).Build();
        (string key, double value) = idValuePair;
        
        producer.Produce(kafkaTopic, new() {Key = key, Value = value}, DeliveryHandler);
        producer.Flush(TimeSpan.FromMilliseconds(flushTimeout));
    }

    private void DeliveryHandler(DeliveryReport<string, double> report)
    {
        if(report.Error.Code != ErrorCode.NoError)
            _logger.LogError($"A error occurs: Code {report.Error.Code}; Reason: {report.Error.Reason}");
        else
            _logger.LogInformation($"A message has been sended to broker: Message: {report.Message}; Key: {report.Key}\n" +
                                   $"Timestamp: {report.Timestamp.UtcDateTime.ToString(CultureInfo.CurrentCulture)}");
    }
}