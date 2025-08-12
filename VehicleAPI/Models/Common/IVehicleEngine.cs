using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common;

public interface IVehicleEngine
{
    public string Type { get; set; }

    public string Abrv { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
