using Vehicle.Models.Common;

namespace Vehicle.Service.Common;

public interface IVehicleEngineService
{
    Task<IEnumerable<IVehicleEngine>> GetEngineAsync();

    Task<IEnumerable<IVehicleEngine>> GetEngineByIdAsync(int id);

    Task<IEnumerable<IVehicleEngine>> GetEngineByNameAsync(string type);
}
