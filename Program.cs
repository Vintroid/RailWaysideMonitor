using RailWaysideMonitor.Types;
using RailWaysideMonitor.Enums;
using RailWaysideMonitor.Services;

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

// Handler with access to lists
var eventHandler = new WaysideEventHandler();

// Endpoint for devices
app.MapGet("/api/devices", () =>
{
    Console.WriteLine("GET /api/devices");   

    return eventHandler.devices;
})
.WithName("GetWaysideDevices");

// Endpoint for events
app.MapGet("/api/events", () =>
{
    Console.WriteLine("GET /api/events");
    return eventHandler.events;
})
.WithName("GetWaysideEvents");

// Endpoint for alerts
app.MapGet("/api/alerts", () =>
{
    Console.WriteLine("GET /api/alerts");
    return eventHandler.alerts;
})
.WithName("GetWaysideAlerts");

// Post for events. Device state should be changed accordingly.
app.MapPost("/api/events", (WaysideEvent newEvent) =>
{   
    Console.WriteLine("POST /api/events");

    var result = eventHandler.HandleEvent(newEvent);

    if (result == Result.RESULT_NOT_FOUND)
    {
        return Results.NotFound("Device not found");
    }

    if (result == Result.RESULT_BAD_REQUEST)
    {
        return Results.BadRequest($"Invalid device state: {newEvent.NewState}");
    }

    return Results.Ok(newEvent);
})
.WithName("PostWaysideEvent");

app.Run();

