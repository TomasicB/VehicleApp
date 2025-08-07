using Vehicle.Models.Common;

namespace Vehicle.Repository.Common;

public interface IVehicleEngineRepository
{
    Task<IEnumerable<IVehicleEngine>> GetEngine();

    Task<IEnumerable<IVehicleEngine>> GetEngineByName(string type);

    Task InsEngine(IVehicleEngine e);

    Task DelEngine(int id);

    Task UpdEngine(int id, IVehicleEngine UpdEngine);
}
