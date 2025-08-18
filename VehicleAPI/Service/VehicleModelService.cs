using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleModelService : IVehicleModelService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleModelService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleModel>> GetModelsAsync() => await _unitOfWork.ModelRepo.GetModelsAsync();

    public async Task<IEnumerable<IVehicleModel>> GetModelByIdAsync(int id) => await _unitOfWork.ModelRepo.GetModelByIdAsync(id);

    public async Task<IEnumerable<IVehicleModel>> GetModelsByNameAsync(string name) => await _unitOfWork.ModelRepo.GetModelsByNameAsync(name);

    public async Task<IEnumerable<IVehicleModel>> GetModelsByMakeAsync(string make) => await _unitOfWork.ModelRepo.GetModelsByMakeAsync(make);

    public async Task InsertModelAsync(IVehicleModel m, int makeid)
    {
        await _unitOfWork.ModelRepo.InsertModelAsync(m, makeid);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteModelAsync(int id)
    {
        await _unitOfWork.ModelRepo.DeleteModelAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateModelAsync(int id, IVehicleModelWrite UpdModel)
    {
        await _unitOfWork.ModelRepo.UpdateModelAsync(id, UpdModel);
        await _unitOfWork.CommitAsync();
    }
}
