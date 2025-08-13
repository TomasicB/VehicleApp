using Vehicle.Models.Common;

namespace Vehicle.Service.Common;

public interface IVehicleEngineService
{
    Task<IEnumerable<IVehicleEngine>> GetEngine();

    Task<IEnumerable<IVehicleEngine>> GetEngineById(int id);

    Task<IEnumerable<IVehicleEngine>> GetEngineByName(string type);

    Task InsEngine(IVehicleEngine e);

    Task DelEngine(int id);

    Task UpdEngine(int id, IVehicleEngine UpdEngine);
}
