package config

import (
	"io/ioutil"
	config_models "luanroger/hardware-streaming/config/models"

	"gopkg.in/yaml.v3"
)

const (
	DefaultConfigPath string = "./hardware_broadcast_config.yaml"
)

var ConfigPref *config_models.YamlConfig

func LoadFromFile(filepath string) error {
	yaml_text, read_err := ioutil.ReadFile(filepath)
	if read_err != nil {
		return read_err
	}

	yaml_err := yaml.Unmarshal([]byte(yaml_text), &ConfigPref)
	if yaml_err != nil {
		return yaml_err
	}

	return nil
}

func SaveToFile() error {
	pref := &config_models.YamlConfig{
		"Elasticsearch",
		config_models.KafkaDomainConfiguration{"localhost:9092", "hardware-streaming", "hardware"},
		config_models.InformationFormat{0, 0, 0, 0, 0, 0},
	}

	yaml, err := yaml.Marshal(pref)

	if err != nil {
		return err
	}

	ioutil.WriteFile(DefaultConfigPath, []byte(yaml), 0644)

	return nil
}
