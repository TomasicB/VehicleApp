using Vehicle.DAL.Entities;
using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleModelDTO : IVehicleModel
{
    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public required VehicleMake VehicleMake { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
