using Vehicle.DAL.Entities;
using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleMakeDTO : IVehicleMake
{
    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleModel>? VehicleModels { get; }
}
