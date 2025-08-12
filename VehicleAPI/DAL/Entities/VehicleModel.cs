namespace Vehicle.DAL.Entities;

public class VehicleModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public int VehicleMakeId { get; set; }

    public required VehicleMake VehicleMake { get; set; }

    public IEnumerable<VehicleRegistration>? VehicleRegistrations { get; }
}
