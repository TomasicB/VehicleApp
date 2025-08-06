using Vehicle.Models.Common;

namespace Vehicle.Models.DTO;

public class VehicleMakeDTO : IVehicleMake
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;
}
