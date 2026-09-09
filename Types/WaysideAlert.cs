namespace RailWaysideMonitor.Types;

public class WaysideAlert
{
    public required string DeviceId {get; set;}
    public required string Message {get;set;}
    public required DateTime Timestamp {get; set;}
}