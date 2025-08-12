using Vehicle.Models.Common.Write;

namespace Vehicle.Models.DTOs.Write;

public class VehicleRegistrationWriteDTO : IVehicleRegistrationWrite
{
    public int Id { get; set; }

    public required string RegistrationNumber { get; set; }

    public required VehicleOwnerWriteDTO VehicleOwner { get; set; }
    
    public required VehicleModelWriteDTO VehicleModel { get; set; }

    public required VehicleEngineWriteDTO VehicleEngine { get; set; }
}
