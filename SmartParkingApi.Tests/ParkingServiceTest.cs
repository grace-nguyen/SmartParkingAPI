
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

    [Fact]
    public async Task AddVehicle_DuplicateLicensePlate_ShouldThrowException()
    {
        //Arrange
        var context = GetDatabaseContext();
        var service = new ParkingService(context);
        var v1 = new Car("20A-4535", DateTime.Now, 30);
        var v2 = new Car("20A-4535", DateTime.Now, 20);

        //Act
        await service.AddVehicleAsync(v1);

        //Assert
        //Check if the system throws an error (Exception) when add a second vehicle with the same license plate number.
        await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddVehicleAsync(v2));
    }

    [Fact]
    public async Task CheckoutVehicle_ExistingVehicle_ShouldReturnVehicleAndRemoveFromDb()
    {
        //Arrange
        var context = GetDatabaseContext();
        var service = new ParkingService(context);
        var plate = "30A-3546";
        await service.AddVehicleAsync(new Car(plate, DateTime.Now, 30));

        //Act
        var result = await service.CheckoutVehicleAsync(plate);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(plate, result.LicensePlate);

        //Check the database to see if the car is missing
        var allVehicle = await service.GetVehiclesAsync();
        Assert.Empty(allVehicle);
    }

}

