using CommandLine;

namespace HardwareStreaming.ArgsHandling;

public class ArgsOptions
{
    [Option('c', "config-file-path", Required = false, Default = Consts.DEFAULT_CONFIGURATION_PATH)]
    public string fileConfigPath { get; set; }
    
    [Option('f', "create-config-file", Required = false, Default = false)]
    public bool createConfigFile { get; set; }
}