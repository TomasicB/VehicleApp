using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common;

public interface IVehicleRegistration
{
    public string RegistrationNumber { get; set; }

    public VehicleOwnerDTO VehicleOwner { get; set; }

    public VehicleModelDTO VehicleModel { get; set; }

    public VehicleEngineDTO VehicleEngine { get; set; }
}
