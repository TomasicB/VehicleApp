using Vehicle.Models.Common;

namespace Vehicle.Service.Common;
public interface IVehicleOwnerService
{
    Task<IEnumerable<IVehicleOwner>> GetOwners();

    Task<IEnumerable<IVehicleOwner>> GetOwnerById(int id);

    Task<IEnumerable<IVehicleOwner>> GetOwnerByName(string name);

    Task InsOwner(IVehicleOwner o);

    Task DelOwner(int id);

    Task UpdOwner(int id, IVehicleOwner UpdOwner);
}
