package main

import (
	"errors"
	"luanroger/hardware-streaming/config"
	kafka_domain "luanroger/hardware-streaming/domains"
	"luanroger/hardware-streaming/logger"
	"os"
)

func main() {
	logger.InitLogger()

	config_path := config.DefaultConfigPath
	if len(os.Args) >= 2 {
		config_path = os.Args[1]
	}
	if err := config.LoadFromFile(config_path); err != nil {
		logger.LogFatal(errors.New("There is no configuration file in " + config_path))
		os.Exit(1)
	} else {
		logger.LogInformation("Configuration loaded from: " + config_path)
	}

	kafka_domain.InitKafkaConsumer()
	kafka_domain.ListenMessages()
}
