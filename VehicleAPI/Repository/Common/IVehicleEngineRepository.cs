using Vehicle.Models.Common;
using Vehicle.Models.DTOs;

namespace Vehicle.Repository.Common;

public interface IVehicleEngineRepository
{
    Task<IEnumerable<IVehicleEngine>> GetEngineAsync();

    Task<IEnumerable<IVehicleEngine>> GetEngineByIdAsync(int id);

    Task<IEnumerable<IVehicleEngine>> GetEngineByNameAsync(string type);
}
