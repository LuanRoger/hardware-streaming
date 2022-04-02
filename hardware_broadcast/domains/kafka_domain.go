package kafka_domain

import (
	"context"
	"encoding/binary"
	"fmt"
	"luanroger/hardware-streaming/config"
	"luanroger/hardware-streaming/logger"
	"math"
	"time"

	"github.com/segmentio/kafka-go"
)

var consumer *kafka.Reader

func InitKafkaConsumer() {
	consumer = kafka.NewReader(kafka.ReaderConfig{
		Brokers:        []string{config.ConfigPref.KafkaConfiguration.BootstrapServer},
		Topic:          config.ConfigPref.KafkaConfiguration.Topic,
		GroupID:        config.ConfigPref.KafkaConfiguration.GroupId,
		CommitInterval: time.Second,
	})
}
func ListenMessages() {
	context := context.Background()
	for {
		msg, err := consumer.FetchMessage(context)

		if err != nil {
			logger.LogError(err)
			break
		}

		key := string(msg.Key)
		value_bits := binary.LittleEndian.Uint32(msg.Value)
		value := math.Float32frombits(value_bits)

		info_map := make(map[string]float32)
		info_map[key] = value

		logger.LogInformation("Key: " + key + fmt.Sprintf("%s%f", " Value: ", value))

		err = consumer.CommitMessages(context, msg)
		if err != nil {
			logger.LogFatal(err)
			break
		}
	}

	consumer.Close()
}
