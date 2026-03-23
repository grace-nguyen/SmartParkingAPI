using SmartParkingApi.Interfaces;
using System.Text.Json.Serialization;


namespace SmartParkingApi.Models;

[JsonDerivedType(typeof(Car), typeDiscriminator: "car")]
[JsonDerivedType(typeof(Van), typeDiscriminator: "van")]
[JsonDerivedType(typeof(Motorbike), typeDiscriminator: "motorbike")]  
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

