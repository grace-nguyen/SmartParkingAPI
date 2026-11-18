
namespace SmartParkingApi.Models;

public class Motorbike : Vehicle
{
  public Motorbike(string licensePlate, DateTime entryTime, decimal hourlyRate) : base(licensePlate, entryTime, hourlyRate) { }
  
  
}
