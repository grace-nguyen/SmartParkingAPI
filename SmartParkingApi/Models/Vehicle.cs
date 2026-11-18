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
  public virtual decimal HourlyRate { get; set; }

  public Vehicle(string licensePlate, DateTime entryTime, decimal hourlyRate)
  {
    LicensePlate = licensePlate;
    EntryTime = entryTime;
    HourlyRate = hourlyRate;
  }
  public virtual decimal CalculateParkingFee(DateTime? currentTime = null)
  {
    DateTime endTime = currentTime ?? DateTime.Now;
    TimeSpan duration= endTime - EntryTime;

    if (duration.TotalSeconds <= 0) return 0m;

    decimal halfHourBlocks = (decimal)Math.Ceiling(duration.TotalMinutes / 30.0);
    
    return halfHourBlocks * (HourlyRate/2m);
  }
}

