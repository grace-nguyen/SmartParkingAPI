using SmartParkingApi.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Register ParkingService as a singleton
builder.Services.AddSingleton<IParkingService, ParkingService>();

builder.Services.AddEndpointsApiExplorer();
// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Configure RouteOptions
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true; // Use lowercase URLs
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Map controller routes
app.MapControllers();

app.Run();