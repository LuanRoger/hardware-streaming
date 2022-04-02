package config

type KafkaDomainConfiguration struct {
	BootstrapServer string `yaml:"bootstrap_server"`
	Topic           string `yaml:"topic"`
	GroupId         string `yaml:"group_id"`
}
