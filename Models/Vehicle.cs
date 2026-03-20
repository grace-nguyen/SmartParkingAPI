namespace SmartParkingApi.Models;

using SmartParkingApi.Interfaces;

public abstract class Vehicle : IParkingVehicle
{
  public string LicensePlate { get; set; }
  public DateTime EntryTime { get; set; }
  public virtual double HourlyRate { get; set; }

  public Vehicle(string licensePlate, DateTime entryTime, double hourlyRate)
  {
    LicensePlate = licensePlate;
    EntryTime = entryTime;
    HourlyRate = hourlyRate;
  }
  public virtual double CalculateParkingFee()
  {
    TimeSpan parkingDuration = DateTime.Now - EntryTime;
    double totalHours = Math.Ceiling(parkingDuration.TotalHours);
    return totalHours * HourlyRate;
  }
}

