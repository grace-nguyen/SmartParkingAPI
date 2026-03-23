using System.Text.Json;
using SmartParkingApi.Models;

namespace SmartParkingApi.Services;

public class ParkingService : IParkingService
{
  // In-memory storage for parked vehicles
  private readonly List<Vehicle> _vehicles = new();
  private readonly string _filePath;

  public ParkingService()
  {
    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
    _filePath = Path.Combine(baseDir, "Data", "parking.json");
    LoadData();
  }

  private void LoadData()
  {
    if (File.Exists(_filePath))
    {
      var jsonData = File.ReadAllText(_filePath);
      var loadVehicles = JsonSerializer.Deserialize<List<Vehicle>>(jsonData);
      if (loadVehicles != null)
      {
        _vehicles.Clear();
        _vehicles.AddRange(loadVehicles);
      }
    }
  }

  private void SaveData()
  {
    // Ensure the Data directory exists before saving
    string? dataDir = Path.GetDirectoryName(_filePath);
    if (dataDir != null)
    {
      Directory.CreateDirectory(dataDir);
    }
    var jsonData = JsonSerializer.Serialize(_vehicles, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(_filePath, jsonData);
  }


  public Task<List<Vehicle>> GetVehiclesAsync() => Task.FromResult(_vehicles);

  public Task AddVehicleAsync(Vehicle vehicle)
  {
    _vehicles.Add(vehicle);
    SaveData();
    return Task.CompletedTask;
  }

  public async Task<Vehicle?> CheckoutVehicleAsync(string licensePlate)

  {
    var vehicle = _vehicles.FirstOrDefault(v => v.LicensePlate.Equals(licensePlate, StringComparison.OrdinalIgnoreCase));
    if (vehicle != null)
    {
      _vehicles.Remove(vehicle);
      SaveData();
    }
    return await Task.FromResult(vehicle);
  }
}