using RailWaysideMonitor.Types;
using RailWaysideMonitor.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// We want for enum names to get converted to strings
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new System.Text.Json.Serialization.JsonStringEnumConverter()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

List<WaysideDevice> devices =
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

List<WaysideEvent> events =
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

// Endpoint for devices
app.MapGet("/api/devices", () =>
{
    Console.WriteLine("GET /api/devices");   

    return devices;
})
.WithName("GetWaysideDevices");

// Endpoint for events
app.MapGet("/api/events", () =>
{
    Console.WriteLine("GET /api/events");
    return events;
})
.WithName("GetWaysideEvents");

// Post for events. Device state should be changed accordingly.
app.MapPost("/api/events", (WaysideEvent newEvent) =>
{   
    Console.WriteLine("POST /api/events");
    
    // checking for corresponding device
    var device = devices.FirstOrDefault(device => newEvent.DeviceId == device.Id);

    if(device == null)
    {
        return null;
    } 
    
    // updating device state
    if(Enum.TryParse<DeviceState>(newEvent.NewState,true, out DeviceState parsedState))
    {
        device.State = parsedState;
    }

    events.Add(newEvent);
    return newEvent;
})
.WithName("PostWaysideEvent");

app.Run();

