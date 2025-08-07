using Vehicle.DAL.Entities;

namespace Vehicle.Models.Common;

public interface IVehicleEngine
{
    public string Type { get; set; }

    public string Abrv { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
