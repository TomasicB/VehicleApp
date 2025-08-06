namespace Vehicle.DAL.Entities;

public class VehicleMakeDTO
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;
}
