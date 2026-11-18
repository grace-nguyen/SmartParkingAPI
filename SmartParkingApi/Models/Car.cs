
namespace SmartParkingApi.Models;

public class Car : Vehicle
{
  public Car(string licensePlate, DateTime entryTime, decimal hourlyRate) : base(licensePlate, entryTime, hourlyRate) {}
}
    