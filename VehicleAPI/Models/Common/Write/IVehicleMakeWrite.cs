using Vehicle.Models.DTOs;

namespace Vehicle.Models.Common.Write;

public interface IVehicleMakeWrite
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Abrv { get; set; }

    public IEnumerable<VehicleModelDTO>? VehicleModels { get; }
}
