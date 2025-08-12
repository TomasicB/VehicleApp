using Vehicle.Models.Common;

namespace Vehicle.Models.DTOs;

public class VehicleEngineDTO : IVehicleEngine
{
    public required string Type { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
