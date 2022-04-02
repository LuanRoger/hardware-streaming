package config

type InformationFormat struct {
	Temperature         int `yaml:"temperature"`
	RamMemoryFormat     int `yaml:"ram_memory_format"`
	CpuUsageFormat      int `yaml:"cpu_usage_format"`
	GpuUsageFormat      int `yaml:"gpu_usage_format"`
	HddUsageFormat      int `yaml:"hdd_usage_format"`
	EthernetUsageFormat int `yaml:"ethernet_usage_format"`
}
