namespace RailWaysideMonitor.Services;
using RailWaysideMonitor.Types;
using RailWaysideMonitor.Enums;

public class WaysideEventHandler
{
    public List<WaysideDevice> devices =
    [
        new() {
            Id = "TRACK-A",
            Type = "TrackCircuit",
            State = DeviceState.Cleared
        },

        new() {
            Id = "SIGNAL-A",
            Type = "Signal",
            State = DeviceState.Occupied
        },

        new() {
            Id = "SWITCH-A",
            Type = "Switch",
            State = DeviceState.Warning
        }

    ];

    public List<WaysideEvent> events =
    [
        new(){
            DeviceId = "TRACK-A",
            NewState = "Occupied",
            Timestamp = new DateTime(2026,9,8,13,59,1)
        },

        new(){
            DeviceId = "SIGNAL-A",
            NewState = "Red",
            Timestamp = new DateTime(2026,9,8,16,22,14)
        },

        new(){
            DeviceId = "SWITCH-A",
            NewState = "Reverse",
            Timestamp = new DateTime(2026,9,8,8,43,45)
        }

    ];

    public List<WaysideAlert> alerts = [];

    public Result HandleEvent(WaysideEvent newEvent)
    {
        // checking for corresponding device
        var device = devices.FirstOrDefault(device => newEvent.DeviceId == device.Id);

        if (device == null)
        {
            return Result.RESULT_NOT_FOUND;
        } 
        
        // Check to see if state to update is valid
        if (!Enum.TryParse<DeviceState>(newEvent.NewState,true, out DeviceState parsedState))
        {
            return Result.RESULT_BAD_REQUEST;
        }

        // modify device state if event has different one.
        if (parsedState != device.State)
        {
            // Alert handling
            if (device.Type == "TrackCircuit" && parsedState == DeviceState.Occupied)
            {
                alerts.Add(new()
                {
                    DeviceId = device.Id,
                    Message = $"Track section {device.Id} is occupied.",
                    Timestamp = newEvent.Timestamp
                });
            }

            device.State = parsedState;
        }
        
        events.Add(newEvent);

        return Result.RESULT_SUCCESS;
    }

}