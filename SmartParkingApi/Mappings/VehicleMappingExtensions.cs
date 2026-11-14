using SmartParkingApi.Models;
using SmartParkingApi.DTOs;

namespace SmartParkingApi.Mappings;

public static class VehicleMappingExtensions
{
    public static VehicleResponseDto? ToDto(this Vehicle vehicle)
    {
        if (vehicle == null) return null;

        return new VehicleResponseDto
        {
            LicensePlate = vehicle.LicensePlate,
            VehicleType = vehicle.GetType().Name,
            EntryTime = vehicle.EntryTime,
            HourlyRate = vehicle.HourlyRate
        };
    }
}