using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Repository.Common;
public interface IVehicleOwnerRepository
{
    Task<IEnumerable<VehicleOwnerDTO>> GetOwners();

    Task<IEnumerable<VehicleOwnerDTO>> GetOwnerByName(string name);

    Task InsOwner(VehicleOwnerDTO o);

    Task DelOwner(int id);

    Task UpdOwner(int id, VehicleOwnerWriteDTO UpdOwner);
}
