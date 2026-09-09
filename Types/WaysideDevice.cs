namespace RailWaysideMonitor.Types;
using RailWaysideMonitor.Enums;

public class WaysideDevice
{
    public required string Id {get; set;}
    public required string Type {get; set;}
    public required DeviceState State {get; set;}

}