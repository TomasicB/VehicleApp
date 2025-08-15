using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleModelService : IVehicleModelService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleModelService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleModel>> GetModels() => await _unitOfWork.ModelRepo.GetModels();

    public async Task<IEnumerable<IVehicleModel>> GetModelById(int id) => await _unitOfWork.ModelRepo.GetModelById(id);

    public async Task<IEnumerable<IVehicleModel>> GetModelByName(string name) => await _unitOfWork.ModelRepo.GetModelByName(name);

    public async Task<IEnumerable<IVehicleModel>> GetModelByMake(string make) => await _unitOfWork.ModelRepo.GetModelByMake(make);

    public async Task InsModel(IVehicleModel m, int makeid)
    {
        await _unitOfWork.ModelRepo.InsModel(m, makeid);
        await _unitOfWork.CommitAsync();
    }

    public async Task DelModel(int id)
    {
        await _unitOfWork.ModelRepo.DelModel(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdModel(int id, IVehicleModelWrite UpdModel)
    {
        await _unitOfWork.ModelRepo.UpdModel(id, UpdModel);
        await _unitOfWork.CommitAsync();
    }
}
