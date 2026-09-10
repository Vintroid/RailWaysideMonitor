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

        // verifications
        Assert.Equal(DeviceState.Cleared,device.State);
    }

    [Fact]
    public void HandleEvent_ValidEvent_UpdatesDeviceState()
    {
        var eventHandler = new WaysideEventHandler();

        WaysideEvent newEvent = new()
        {
            DeviceId = "TRACK-A",
            NewState = "Occupied",
            Timestamp = new DateTime(2026,9,9,12,50,43)
        };

        var result = eventHandler.HandleEvent(newEvent);

        var device = eventHandler.devices.FirstOrDefault(device => newEvent.DeviceId == device.Id);

        // verifications
        Assert.NotNull(device);
        Assert.Equal("TRACK-A", device.Id);
        Assert.Equal(DeviceState.Occupied, device.State);
        Assert.Equal(Result.RESULT_SUCCESS, result);
    }
}
