using Vehicle.Models.Common.Write;

namespace Vehicle.Models.DTOs.Write;

public class VehicleModelWriteDTO : IVehicleModelWrite
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public required VehicleMakeWriteDTO VehicleMake { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
