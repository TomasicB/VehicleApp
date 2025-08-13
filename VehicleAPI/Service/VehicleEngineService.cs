using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleEngineService : IVehicleEngineService
{
    private readonly IVehicleEngineRepository _engineRepo;

    public VehicleEngineService(IVehicleEngineRepository engineRepo) => _engineRepo = engineRepo;

    public async Task<IEnumerable<IVehicleEngine>> GetEngine() => await _engineRepo.GetEngine();

    public async Task<IEnumerable<IVehicleEngine>> GetEngineById(int id) => await _engineRepo.GetEngineById(id);

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByName(string type) => await _engineRepo.GetEngineByName(type);

    public async Task InsEngine(IVehicleEngine e) => await _engineRepo.InsEngine(e);

    public async Task DelEngine(int id) => await _engineRepo.DelEngine(id);

    public async Task UpdEngine(int id, IVehicleEngine UpdEngine) => await _engineRepo.UpdEngine(id, UpdEngine);
}
