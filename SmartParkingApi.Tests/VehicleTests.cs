using SmartParkingApi.Models;

namespace SmartParkingApi.Tests;

public class VehicleTests
{
    [Theory]
    [InlineData(120, 0, 80)]
    [InlineData(120, 1, 120)]
    [InlineData(15, 0, 40)]
    [InlineData(0, 0, 0)]
    public void CalculateParkingFee_VariousScenarios_ShouldReturnExpectedFee(int minutes, int seconds, double expectedFee)
    {
        //1. Arrange
        double hourlyRate = 40;
        var entry = new DateTime(2026, 1, 1, 10, 0, 0);
        var exit = entry.AddMinutes(minutes).AddSeconds(seconds);
        var car = new Car("30A-5633", entry, hourlyRate);

        //2. Act
        var fee = car.CalculateParkingFee(exit);

        //3. Assert
        Assert.Equal(expectedFee, fee);
    }
}