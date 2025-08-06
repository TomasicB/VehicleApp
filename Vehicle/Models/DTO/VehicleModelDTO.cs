using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleModelDTO : IVehicleModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public int VehicleMakeId { get; set; }
}
