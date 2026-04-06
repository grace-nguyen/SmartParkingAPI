
using Microsoft.EntityFrameworkCore;
using SmartParkingApi.Data;
using SmartParkingApi.Models;
using SmartParkingApi.Services;
using Xunit;

namespace SmartParkingApi.Tests;

public class ParkingServiceTests
{
    private ApplicationDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var databaseContext = new ApplicationDbContext(options);
        databaseContext.Database.EnsureCreated();

        return databaseContext;
    }

    [Fact]
    public async Task AddVehicleAsync_ShouldAddVehicleToDatabase()
    {
        // 1.Arrange
        var context = GetDatabaseContext();
        var service = new ParkingService(context);
        var vehicle = new Car ( "34F-34625", DateTime.Now, 40);

        // 2. Act 
        await service.AddVehicleAsync(vehicle);

        // 3/ Assert
        var result = await service.GetVehiclesAsync();
        Assert.Single(result);
        Assert.Equal("34F-34625", result[0].LicensePlate);
    }
}

