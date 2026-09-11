using RailWaysideMonitor.Enums;
using RailWaysideMonitor.Types;
using RailWaysideMonitor.Services;

namespace RailWaysideMonitor.Tests;

public class UnitTest1
{
    // Initialize directly with wanted state.
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

    // Add event that modifies device state.
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

    // Test with device id not found, no event should be added
    [Fact]
    public void HandleEvent_UnknownDevice_ReturnsNotFound()
    {
        var eventHandler = new WaysideEventHandler();

        var countEvents = eventHandler.events.Count;

        var newEvent = new WaysideEvent()
        {
            DeviceId = "TRACK-NOT-FOUND",
            NewState = "Occupied",
            Timestamp = new DateTime(2026,9,9,12,50,43)
        };

        var result = eventHandler.HandleEvent(newEvent);

        // verifications
        Assert.Equal(Result.RESULT_NOT_FOUND, result);
        Assert.Equal(countEvents, eventHandler.events.Count);
    }

    // Test with invalid event state, no event should be added
    [Fact]
    public void HandleEvent_InvalidState_ReturnsBadRequest()
    {
        var eventHandler = new WaysideEventHandler();

        var countEvents = eventHandler.events.Count;

        var newEvent = new WaysideEvent()
        {
            DeviceId = "TRACK-A",
            NewState = "InvalidState",
            Timestamp = new DateTime(2026,9,9,12,50,43)
        };

        var result = eventHandler.HandleEvent(newEvent);
        
        var device = eventHandler.devices.FirstOrDefault(device => newEvent.DeviceId == device.Id);

        // verifications
        Assert.NotNull(device);
        Assert.Equal(Result.RESULT_BAD_REQUEST, result);
        Assert.Equal(countEvents, eventHandler.events.Count);
        Assert.Equal(DeviceState.Cleared, device.State);
    }

    // Test if device TRACK-A state change from Cleared to Occupied creates an alert properly.
    [Fact]
    public void HandleEvent_TrackBecomesOccupied_CreatesAlert()
    {
        var eventHandler = new WaysideEventHandler();

        var alertCount = eventHandler.alerts.Count;

        var newEvent = new WaysideEvent()
        {
            DeviceId = "TRACK-A",
            NewState = "Occupied",
            Timestamp = new DateTime(2026,9,10,13,45,45)
        };

        var result = eventHandler.HandleEvent(newEvent);

        // verifications
        Assert.Equal(Result.RESULT_SUCCESS, result);
        Assert.Equal(alertCount + 1, eventHandler.alerts.Count);
        Assert.Equal("TRACK-A", eventHandler.alerts.Last().DeviceId);
    }
}
