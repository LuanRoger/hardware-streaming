package config

type YamlConfig struct {
	Engine                         string `yaml:"engine"`
	KafkaConfiguration             KafkaDomainConfiguration `yaml:"kafka_configuration"`
	InformationFormatConfiguration InformationFormat `yaml:"information_format"`
}