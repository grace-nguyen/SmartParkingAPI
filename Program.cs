using SmartParkingApi.Services;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using SmartParkingApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Config DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Register ParkingService as a AddScoped
builder.Services.AddScoped<IParkingService, ParkingService>();

builder.Services.AddEndpointsApiExplorer();
// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Configure RouteOptions
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true; // Use lowercase URLs
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<SmartParkingApi.Middlewares.ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Map controller routes
app.MapControllers();

app.Run();