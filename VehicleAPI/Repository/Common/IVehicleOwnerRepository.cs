using Vehicle.Models.Common;

namespace Vehicle.Repository.Common;
public interface IVehicleOwnerRepository
{
    Task<IEnumerable<IVehicleOwner>> GetOwnersAsync();

    Task<IEnumerable<IVehicleOwner>> GetOwnerByIdAsync(int id);

    Task<IEnumerable<IVehicleOwner>> GetOwnersByNameAsync(string name);

    Task InsertOwnerAsync(IVehicleOwner o);

    Task DeleteOwnerAsync(int id);

    Task UpdateOwnerAsync(int id, IVehicleOwner UpdOwner);
}
