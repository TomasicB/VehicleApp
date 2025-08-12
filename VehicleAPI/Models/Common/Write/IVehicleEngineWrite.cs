using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common.Write;

public interface IVehicleEngineWrite
{
    public int Id { get; set; }

    public string Type { get; set; }

    public string Abrv { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
