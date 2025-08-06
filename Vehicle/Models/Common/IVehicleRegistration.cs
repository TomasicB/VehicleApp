namespace Vehicle.Models.Common;

public interface IVehicleRegistration
{
    public string RegistrationNumber { get; set; }

    public int VehicleModelId { get; set; }

    public int VehicleEngineId { get; set; }

    public int VehicleOwnerId { get; set; }
}
