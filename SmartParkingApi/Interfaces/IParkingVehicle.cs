namespace SmartParkingApi.Interfaces;
  public interface IParkingVehicle
{
  public double CalculateParkingFee(DateTime? currentTime = null);
}