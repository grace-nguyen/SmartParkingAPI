using SmartParkingApi.Models;

namespace SmartParkingApi.Tests;

public class VehicleTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 3.5)]
    [InlineData(30, 0, 3.5)]
    [InlineData(30, 1, 7)]
    [InlineData(60, 0, 7)]
    [InlineData(75, 0, 10.5)]
    [InlineData(90, 0, 10.5)]
    [InlineData(120, 0, 14)]
    [InlineData(120, 1, 17.5)]
    public void CalculateParkingFee_VariousScenarios_ShouldReturnExpectedFee(int minutes, int seconds, decimal expectedFee)
    {
        //1. Arrange
        var entry = new DateTime(2026, 1, 1, 10, 0, 0);
        var exit = entry.AddMinutes(minutes).AddSeconds(seconds);

        var car = new Car("30A-5633", entry, 7m);

        //2. Act
        decimal fee = car.CalculateParkingFee(exit);

        //3. Assert
        Assert.Equal(expectedFee, fee);
    }

    [Theory]
    [InlineData(4, 2)]
    [InlineData(7, 3.5)]
    [InlineData(10, 5)]
    public void CalculateParkingFee_WithDifferentRates_ShouldReturnExpectedFee(decimal hourlyRate, decimal expectedFee)
    {
        // Arrange
        var entry = new DateTime(2026, 1, 1, 10, 0, 0);
        var exit =  entry.AddMinutes(30);

        var car = new Car("WKD-679", entry, hourlyRate);

        //Act
        decimal fee = car.CalculateParkingFee(exit);

        //Assert 
        Assert.Equal(expectedFee, fee);
    }
}