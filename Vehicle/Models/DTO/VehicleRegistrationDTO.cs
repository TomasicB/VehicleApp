using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleRegistrationDTO : IVehicleRegistration
{
    public int Id { get; set; }

    public required string RegistrationNumber { get; set; }

    public int VehicleModelId { get; set; }

    public int VehicleEngineId { get; set; }

    public int VehicleOwnerId { get; set; }
}
