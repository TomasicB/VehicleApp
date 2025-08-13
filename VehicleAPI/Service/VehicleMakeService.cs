using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly IVehicleMakeRepository _makeRepo;

    public VehicleMakeService(IVehicleMakeRepository makeRepo) => _makeRepo = makeRepo;

    public async Task<IEnumerable<IVehicleMake>> GetMake() => await _makeRepo.GetMake();

    public async Task<IEnumerable<IVehicleMake>> GetMakeById(int id) => await _makeRepo.GetMakeById(id);

    public async Task<IEnumerable<IVehicleMake>> GetMakeByName(string name) => await _makeRepo.GetMakeByName(name);

    public async Task InsMake(IVehicleMake m) => await _makeRepo.InsMake(m);

    public async Task DelMake(int id) => await _makeRepo.DelMake(id);

    public async Task UpdMake(int id, IVehicleMake UpdMake) => await _makeRepo.UpdMake(id, UpdMake);
}
