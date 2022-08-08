using CommandLine;

namespace HardwareBroadcast.ArgsHandling;

public class ArgsOptions
{
    [Option('c', "config-file-path", Required = false, Default = Consts.DEFAULT_CONFIGURATION_PATH)]
    public string fileConfigPath { get; set; }
}