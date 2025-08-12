using Vehicle.Models.Common;

namespace Vehicle.Models.DTOs;

public class VehicleMakeDTO : IVehicleMake
{
    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleModelDTO>? VehicleModels { get; }
}
