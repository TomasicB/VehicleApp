using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common;

public interface IVehicleMake
{
    public string Name { get; set; }

    public string Abrv { get; set; }

    public IEnumerable<VehicleModelDTO>? VehicleModels { get; }
}
