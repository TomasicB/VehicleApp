using Vehicle.Models.Common.Write;

namespace Vehicle.Models.DTOs.Write;

public class VehicleEngineWriteDTO : IVehicleEngineWrite
{
    public int Id { get; set; }

    public required string Type { get; set; }

    public string Abrv { get; set; } = string.Empty;

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
