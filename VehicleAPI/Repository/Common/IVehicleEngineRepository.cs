using Vehicle.Models.Common;
using Vehicle.Models.DTOs;

namespace Vehicle.Repository.Common;

public interface IVehicleEngineRepository
{
    Task<IEnumerable<VehicleEngineDTO>> GetEngine();

    Task<IEnumerable<VehicleEngineDTO>> GetEngineByName(string type);

    Task InsEngine(VehicleEngineDTO e);

    Task DelEngine(int id);

    Task UpdEngine(int id, VehicleEngineDTO UpdEngine);
}
