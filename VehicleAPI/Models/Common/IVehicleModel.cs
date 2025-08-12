using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common;

public interface IVehicleModel
{
    public string Name { get; set; }

    public string Abrv { get; set; }

    public VehicleMakeDTO VehicleMake { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
