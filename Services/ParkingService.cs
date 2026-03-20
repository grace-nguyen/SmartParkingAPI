using SmartParkingApi.Models;

namespace SmartParkingApi.Services;

public class ParkingService : IParkingService
{
  // In-memory storage for parked vehicles
  private readonly List<Vehicle> _vehicles = new();

  public ParkingService()
  {
    // Initialize with some sample vehicles
    _vehicles.Add(new Car("ABC123", DateTime.Now.AddHours(-2), 10.0));
    _vehicles.Add(new Van("XYZ789", DateTime.Now.AddHours(-3), 15.0));
    _vehicles.Add(new Motorbike("MOTO456", DateTime.Now.AddHours(-1), 5.0));
  }

  public Task<List<Vehicle>> GetVehiclesAsync() => Task.FromResult(_vehicles);

  public Task AddVehicleAsync(Vehicle vehicle)
  {
    _vehicles.Add(vehicle);
    return Task.CompletedTask;
  }

  public async Task<Vehicle?> CheckoutVehicleAsync(string licensePlate)

  {
    var vehicle = _vehicles.FirstOrDefault(v => v.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
    if (vehicle != null)
    {
      _vehicles.Remove(vehicle);
    }
    return await Task.FromResult(vehicle);
  }
}