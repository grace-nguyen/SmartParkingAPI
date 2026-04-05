using Microsoft.EntityFrameworkCore;
using SmartParkingApi.Data;
using SmartParkingApi.Models;

namespace SmartParkingApi.Services;

public class ParkingService : IParkingService
{
  private readonly ApplicationDbContext _context;

  public ParkingService(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<List<Vehicle>> GetVehiclesAsync()
  {
    return await _context.Vehicles.ToListAsync();
  }

  public async Task AddVehicleAsync(Vehicle vehicle)
  {
    _context.Vehicles.Add(vehicle);
    await _context.SaveChangesAsync();
  }

  public async Task<Vehicle?> CheckoutVehicleAsync(string licensePlate)

  {
    var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v => v.LicensePlate.ToLower() == licensePlate.ToLower());
    if (vehicle != null)
    {
      _context.Vehicles.Remove(vehicle);
      await _context.SaveChangesAsync();
    }
    return vehicle;
  }
}