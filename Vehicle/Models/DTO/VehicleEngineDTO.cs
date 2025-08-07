using Vehicle.DAL.Entities;
using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleEngineDTO : IVehicleEngine
{
    public required string Type { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
