using Vehicle.DAL.Entities;
using Vehicle.Models.DTO;

public interface IVehicleRegistration
{
    public string RegistrationNumber { get; set; }

    public VehicleOwner VehicleOwner { get; set; }

    public VehicleModel VehicleModel { get; set; }

    public VehicleEngine VehicleEngine { get; set; }
}
