using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartParkingApi.Interfaces;
using SmartParkingApi.Models;
using SmartParkingApi.Services;
using SmartParkingApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using SmartParkingApi.Constants;

namespace SmartParkingApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ParkingController : ControllerBase
{
  private readonly IParkingService _parkingService;
  // Inject the parking service through the constructor
  public ParkingController(IParkingService parkingService)
  {
    _parkingService = parkingService;
  }

  [HttpGet()]
  [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
  public async Task<ActionResult<List<Vehicle>>> GetParkingVehicles()
  {
    // Get the list of vehicles
    var list = await _parkingService.GetVehiclesAsync();

    // Sort the list by parking fee in descending order
    var sortedList = list.OrderByDescending(v => v.CalculateParkingFee()).ToList();

    return Ok(sortedList); // Return the sorted list
  }

  [HttpGet("{licensePlate}")]
  [Authorize(Roles = UserRoles.Admin)]
  public async Task<ActionResult<Vehicle>> GetByPlate(string licensePlate)
  {
    // Get the vehicle by license plate
    var list = await _parkingService.GetVehiclesAsync();
    var vehicle = list.FirstOrDefault(v => v.LicensePlate == licensePlate);

    if (vehicle == null)
    {
      return NotFound(new { message = "Vehicle not found" });
    }

    return Ok(vehicle);
  }


  [HttpPost]
  [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Staff)]
  public async Task<IActionResult> AddVehicle([FromBody] VehicleDto vehicleDto)
  {
    Vehicle? newVehicle = vehicleDto.VehicleType switch
    {
      VehicleType.Car => new Car(vehicleDto.LicensePlate, DateTime.Now, vehicleDto.HourlyRate > 0 ? vehicleDto.HourlyRate : 10.0),
      VehicleType.Van => new Van(vehicleDto.LicensePlate, DateTime.Now, vehicleDto.HourlyRate > 0 ? vehicleDto.HourlyRate : 15.0),
      VehicleType.Motorbike => new Motorbike(vehicleDto.LicensePlate, DateTime.Now, vehicleDto.HourlyRate > 0 ? vehicleDto.HourlyRate : 5.0),
      _ => null
    };

    if (newVehicle == null)
    {
      return BadRequest(new { message = "Invalid vehicle type. Support: car, van, motorbike" });
    }

    // Add the new vehicle to the parking service
    await _parkingService.AddVehicleAsync(newVehicle);

    return CreatedAtAction(nameof(GetParkingVehicles), new { licensePlate = newVehicle.LicensePlate }, new
    {
      message = "Vehicle added successfully",
      data = newVehicle
    });
  }

  [HttpDelete("{licensePlate}")]
  [Authorize(Roles = UserRoles.Admin)]

  public async Task<IActionResult> CheckoutVehicle(string licensePlate)
  {
    var vehicle = await _parkingService.CheckoutVehicleAsync(licensePlate);
    if (vehicle == null)
    {
      return NotFound(new { message = $"Vehicle with license plate {licensePlate} not found" });
    }

    var fee = vehicle.CalculateParkingFee();
    var duration = DateTime.Now - vehicle.EntryTime;
    return Ok(new
    {
      message = "Vehicle checked out successfully",
      data = new
      {
        licensePlate = vehicle.LicensePlate,
        entryTime = vehicle.EntryTime,
        exitTime = DateTime.Now,
        duration = $"{Math.Floor(duration.TotalHours)}h {duration.Minutes}m",
        totalFee = fee.ToString("C2"),
        currency = "AUD"
      }
    });
  }
}




