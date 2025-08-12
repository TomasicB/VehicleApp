namespace Vehicle.DAL.Entities;

public class VehicleEngine
{
    public int Id { get; set; }

    public required string Type { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
