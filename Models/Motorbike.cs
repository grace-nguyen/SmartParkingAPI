
namespace SmartParkingApi.Models;

public class Motorbike : Vehicle
{
  public Motorbike(string licensePlate, DateTime entryTime, double hourlyRate) : base(licensePlate, entryTime, hourlyRate) { }
  
  
}
