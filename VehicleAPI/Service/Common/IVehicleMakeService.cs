using Vehicle.Models.Common;

namespace Vehicle.Service.Common;

public interface IVehicleMakeService
{
    Task<IEnumerable<IVehicleMake>> GetMake();

    Task<IEnumerable<IVehicleMake>> GetMakeById(int id);

    Task<IEnumerable<IVehicleMake>> GetMakeByName(string name);

    Task InsMake(IVehicleMake make);

    Task DelMake(int id);

    Task UpdMake(int id, IVehicleMake UpdMake);
}
