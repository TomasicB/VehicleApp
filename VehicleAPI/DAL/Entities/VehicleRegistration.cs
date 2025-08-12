namespace Vehicle.DAL.Entities;

public class VehicleRegistration
{
    public int Id { get; set; }

    public required string RegistrationNumber { get; set; }

    public int VehicleModelId { get; set; }

    public int VehicleEngineId { get; set; }

    public int VehicleOwnerId { get; set; }

    public required VehicleModel VehicleModel { get; set; }

    public required VehicleEngine VehicleEngine { get; set; }

    public required VehicleOwner VehicleOwner { get; set; }
}
