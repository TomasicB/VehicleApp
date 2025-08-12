using Vehicle.Models.Common;

namespace Vehicle.Models.DTOs;

public class VehicleRegistrationDTO : IVehicleRegistration
{
    public required string RegistrationNumber { get; set; }

    public required VehicleOwnerDTO VehicleOwner { get; set; }

    public required VehicleModelDTO VehicleModel { get; set; }

    public required VehicleEngineDTO VehicleEngine { get; set; }
}
