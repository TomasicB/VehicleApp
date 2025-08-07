using Vehicle.Models.Common;

namespace Vehicle.Repository.Common;

public interface IVehicleMakeRepository
{
    Task<IEnumerable<IVehicleMake>> GetMake();

    Task<IEnumerable<IVehicleMake>> GetMakeByName(string name);

    Task InsMake(IVehicleMake make);

    Task DelMake(int id);

    Task UpdMake(int id, IVehicleMake UpdMake);
}
