using System.Globalization;
using Confluent.Kafka;
using HardwareStreaming.Internals.Loggin;

namespace HardwareStreaming.Domains;

public class KafkaDomain
{
    public ProducerConfig producerConfig { get; }
    private Logger _logger { get; }
    private string kafkaTopic { get; }

    public KafkaDomain(ProducerConfig producerConfig, Logger logger, string kafkaTopicName)
    {
        this.producerConfig = producerConfig;
        _logger = logger;
        kafkaTopic = kafkaTopicName;
    }

    public void StreamInfo(KeyValuePair<string, float> idValuePair, in IProducer<string, float> producer)
    {
        (string key, float value) = idValuePair;
        
        producer.Produce(kafkaTopic, new() {Key = key, Value = value}, DeliveryHandler);
    }

    private void DeliveryHandler(DeliveryReport<string, float> report)
    {
        if(report.Error.Code != ErrorCode.NoError)
            _logger.LogError($"A error occurs: Code {report.Error.Code}; Reason: {report.Error.Reason}");
        else
            _logger.LogInformation("A message has been sended to broker:\n" +
                                   $"Message: {report.Message}; Key: {report.Key}[{report.Value}]\n" +
                                   $"Timestamp: {report.Timestamp.UtcDateTime.ToString(CultureInfo.CurrentCulture)}");
    }
}