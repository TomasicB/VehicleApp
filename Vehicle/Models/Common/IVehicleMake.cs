using Vehicle.DAL.Entities;

namespace Vehicle.Models.Common;

public interface IVehicleMake
{
    public string Name { get; set; }

    public string Abrv { get; set; }

    public IEnumerable<VehicleModel>? VehicleModels { get; }
}
