using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Models.Common.Write;

public interface IVehicleModelWrite
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Abrv { get; set; }

    public VehicleMakeWriteDTO VehicleMake { get; set; }

    public IEnumerable<VehicleRegistrationDTO>? VehicleRegistrations { get; }
}
