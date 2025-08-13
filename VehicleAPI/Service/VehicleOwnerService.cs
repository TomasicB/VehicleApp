using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleOwnerService : IVehicleOwnerService
{
    private readonly IVehicleOwnerRepository _ownerRepo;

    public VehicleOwnerService(IVehicleOwnerRepository ownerRepo) => _ownerRepo = ownerRepo;

    public async Task<IEnumerable<IVehicleOwner>> GetOwners() => await _ownerRepo.GetOwners();

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerById(int id) => await _ownerRepo.GetOwnerById(id);

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerByName(string name) => await _ownerRepo.GetOwnerByName(name);

    public async Task InsOwner(IVehicleOwner o) => await _ownerRepo.InsOwner(o);

    public async Task DelOwner(int id) => await _ownerRepo.DelOwner(id);

    public async Task UpdOwner(int id, IVehicleOwner UpdOwner) => await _ownerRepo.UpdOwner(id, UpdOwner);
}
