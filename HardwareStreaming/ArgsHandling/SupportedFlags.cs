namespace HardwareStreaming.ArgsHandling;

public class SupportedFlags
{
    public static readonly Dictionary<ArgsFlags, string> AVAILABLE_ARGS_FLAGS = new()
    {
        { ArgsFlags.CreateConfigFile, "--create-config-file" }
    };
}