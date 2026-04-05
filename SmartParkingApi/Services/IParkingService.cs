using SmartParkingApi.Models;

namespace SmartParkingApi.Services
{
  public interface IParkingService
  {
    Task<List<Vehicle>> GetVehiclesAsync();
    Task AddVehicleAsync(Vehicle vehicle);
    Task<Vehicle?> CheckoutVehicleAsync(string licensePlate);
  }
}