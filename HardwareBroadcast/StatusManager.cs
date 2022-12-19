namespace HardwareBroadcast;

public enum ServiceStatus { STARTING, RUNNING }

public class StatusManager
{
    public ServiceStatus currentStatus { get; private set; }
    

    public StatusManager()
    {
        currentStatus = ServiceStatus.STARTING;
    }
    
    public void Update(ServiceStatus newStatus)
    {
        currentStatus = newStatus;   
    }
}