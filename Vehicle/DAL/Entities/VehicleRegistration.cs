namespace Vehicle.DAL.Entities;

public class VehicleRegistrationDTO
{
    public int Id { get; set; }

    public required string RegistrationNumber { get; set; }

    public int VehicleModelId { get; set; }

    public int VehicleEngineId { get; set; }

    public int VehicleOwnerId { get; set; }
}
