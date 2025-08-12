using Vehicle.Models.Common.Write;

namespace Vehicle.Models.DTOs.Write;

public class VehicleMakeWriteDTO : IVehicleMakeWrite
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleModelDTO>? VehicleModels { get; }
}
