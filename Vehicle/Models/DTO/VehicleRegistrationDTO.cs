using Vehicle.DAL.Entities;
using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleRegistrationDTO : IVehicleRegistration
{
    public required string RegistrationNumber { get; set; }

    public required VehicleOwner VehicleOwner { get; set; }

    public required VehicleModel VehicleModel { get; set; }

    public required VehicleEngine VehicleEngine { get; set; }
}
