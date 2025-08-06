using Vehicle.Models.Common;

namespace Vehicle.Repository.Common;
public interface IVehicleOwnerRepository
{
    Task<IEnumerable<IVehicleOwner>> GetOwners();

    Task<IEnumerable<IVehicleOwner>> GetOwnerById(string name);

    Task InsOwner(IVehicleOwner o);

    Task DelOwner(int id);

    Task UpdOwner(int id, IVehicleOwner UpdOwner);
}
