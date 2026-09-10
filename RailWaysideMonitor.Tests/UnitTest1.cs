using RailWaysideMonitor.Enums;
using RailWaysideMonitor.Types;
using RailWaysideMonitor.Services;

namespace RailWaysideMonitor.Tests;

public class UnitTest1
{
    [Fact]
    public void WaysideDevice_InitializesWithExpectedState()
    {
        WaysideDevice device = new()
        {
            Id = "TRACK-A",
            Type = "TrackCircuit",
            State = DeviceState.Cleared
        };

        Assert.Equal(DeviceState.Cleared,device.State);
    }
}
