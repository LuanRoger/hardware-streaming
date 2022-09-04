namespace HardwareStreaming.StreamingDataProcessor;

public interface ISensorProcessableValue
{
    public float ProcessValue(ISensorDataProcessor dataProcessor);
}