using Vehicle.Models.Common;

namespace Vehicle.Models.DTOs;

public class VehicleModelDTO : IVehicleModel
{
    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public required VehicleMakeDTO VehicleMake { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
