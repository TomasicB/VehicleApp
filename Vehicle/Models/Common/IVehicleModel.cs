using Vehicle.DAL.Entities;
using Vehicle.Models.DTO;

namespace Vehicle.Models.Common;

public interface IVehicleModel
{
    public string Name { get; set; }

    public string Abrv { get; set; }

    public VehicleMake VehicleMake { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
