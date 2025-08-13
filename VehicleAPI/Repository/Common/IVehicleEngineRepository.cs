using Vehicle.Models.Common;
using Vehicle.Models.DTOs;

namespace Vehicle.Repository.Common;

public interface IVehicleEngineRepository
{
    Task<IEnumerable<VehicleEngineDTO>> GetEngine();

    Task<IEnumerable<VehicleEngineDTO>> GetEngineById(int id);

    Task<IEnumerable<VehicleEngineDTO>> GetEngineByName(string type);

    Task InsEngine(IVehicleEngine e);

    Task DelEngine(int id);

    Task UpdEngine(int id, IVehicleEngine UpdEngine);
}
