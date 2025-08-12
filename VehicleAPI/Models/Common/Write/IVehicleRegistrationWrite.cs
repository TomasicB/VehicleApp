using Vehicle.Models.DTOs.Write;

namespace Vehicle.Models.Common.Write;

public interface IVehicleRegistrationWrite
{
    public int Id { get; set; }

    public string RegistrationNumber { get; set; }

    public VehicleOwnerWriteDTO VehicleOwner { get; set; }

    public VehicleModelWriteDTO VehicleModel { get; set; }

    public VehicleEngineWriteDTO VehicleEngine { get; set; }
}
