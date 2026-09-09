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

var devices = new[]
{
    new WaysideDevice
    {
        Id = "TRACK-A",
        Type = "TrackCircuit",
        State = DeviceState.Cleared
    }
};


app.MapGet("/api/devices", () =>
{
    Console.WriteLine("GET /api/devices");   

    return devices;
})
.WithName("GetWaysideDevices");

app.Run();

