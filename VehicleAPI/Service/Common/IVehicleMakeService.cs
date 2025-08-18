using Vehicle.Models.Common;

namespace Vehicle.Service.Common;

public interface IVehicleMakeService
{
    Task<IEnumerable<IVehicleMake>> GetMakeAsync();

    Task<IEnumerable<IVehicleMake>> GetMakeByIdAsync(int id);

    Task<IEnumerable<IVehicleMake>> GetMakesByNameAsync(string name);

    Task InsertMakeAsync(IVehicleMake make);

    Task DeleteMakeAsync(int id);

    Task UpdateMakeAsync(int id, IVehicleMake UpdMake);
}
