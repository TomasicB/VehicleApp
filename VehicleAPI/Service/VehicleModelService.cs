using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleModelService : IVehicleModelService
{
    private readonly IVehicleModelRepository _modelRepo;

    public VehicleModelService(IVehicleModelRepository modelRepo) => _modelRepo = modelRepo;

    public async Task<IEnumerable<IVehicleModel>> GetModels() => await _modelRepo.GetModels();

    public async Task<IEnumerable<IVehicleModel>> GetModelById(int id) => await _modelRepo.GetModelById(id);

    public async Task<IEnumerable<IVehicleModel>> GetModelByName(string name) => await _modelRepo.GetModelByName(name);

    public async Task<IEnumerable<IVehicleModel>> GetModelByMake(string make) => await _modelRepo.GetModelByMake(make);

    public async Task InsModel(IVehicleModel m, int makeid) => await _modelRepo.InsModel(m, makeid);

    public async Task DelModel(int id) => await _modelRepo.DelModel(id);

    public async Task UpdModel(int id, IVehicleModelWrite UpdModel) => await _modelRepo.UpdModel(id, UpdModel);
}
