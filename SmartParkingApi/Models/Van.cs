namespace SmartParkingApi.Models;

public class Van : Vehicle
{
  public Van(string licensePlate, DateTime entryTime, double hourlyRate) : base(licensePlate, entryTime, hourlyRate) {}
}
  