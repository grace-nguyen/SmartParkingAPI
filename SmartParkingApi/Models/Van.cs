namespace SmartParkingApi.Models;

public class Van : Vehicle
{
  public Van(string licensePlate, DateTime entryTime, decimal hourlyRate) : base(licensePlate, entryTime, hourlyRate) {}
}
  