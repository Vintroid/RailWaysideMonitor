namespace RailWaysideMonitor.Types;

public class WaysideEvent
{
    public required string DeviceId {get; set;}
    public required string NewState {get; set;}
    public required DateTime Timestamp {get; set;}
}