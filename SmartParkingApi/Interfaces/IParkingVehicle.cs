namespace SmartParkingApi.Interfaces;
  public interface IParkingVehicle
{
  public decimal CalculateParkingFee(DateTime? currentTime = null);
}