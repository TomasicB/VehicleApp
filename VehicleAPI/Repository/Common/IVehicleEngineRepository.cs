using Vehicle.Models.Common;
using Vehicle.Models.DTOs;

namespace Vehicle.Repository.Common;

public interface IVehicleEngineRepository
{
    Task<IEnumerable<IVehicleEngine>> GetEngine();

    Task<IEnumerable<IVehicleEngine>> GetEngineById(int id);

    Task<IEnumerable<IVehicleEngine>> GetEngineByName(string type);
}
