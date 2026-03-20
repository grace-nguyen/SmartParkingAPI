
namespace SmartParkingApi.Models;

public class Car : Vehicle
{
  public Car(string licensePlate, DateTime entryTime, double hourlyRate) : base(licensePlate, entryTime, hourlyRate) {}
}
    