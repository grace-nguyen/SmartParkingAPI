using System.ComponentModel.DataAnnotations;

namespace SmartParkingApi.DTOs;

public class VehicleDTO
{
  [Required(ErrorMessage = "License plate is required")]
  [StringLength(10, MinimumLength = 3, ErrorMessage = "License plate must be between 3 and 10 characters")]
  [RegularExpression(@"^[A-Z0-9\-]+$", ErrorMessage = "License plate can only contain uppercase letters, numbers, and hyphens")]
  public required string LicensePlate { get; set; }

  [Required(ErrorMessage = "Vehicle type is required")]
  [EnumDataType(typeof(VehicleType), ErrorMessage = "Vehicle type must be Car, Van, or Motorbike")]
  public required VehicleType VehicleType { get; set; }

  [Range(0, 100, ErrorMessage = "Hourly rate must be a positive number")]
  public double HourlyRate { get; set; }
}