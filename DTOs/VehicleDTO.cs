using System.ComponentModel.DataAnnotations;

namespace SmartParkingApi.DTOs;

public class VehicleDTO
{
  public required string LicensePlate { get; set; }

  [EnumDataType(typeof(VehicleType), ErrorMessage = "Vehicle type must be Car, Van, or Motorbike")]
  public required VehicleType VehicleType { get; set; }
  public double HourlyRate { get; set; }
}