using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleEngineService : IVehicleEngineService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleEngineService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleEngine>> GetEngine() => await _unitOfWork.EngineRepo.GetEngine();

    public async Task<IEnumerable<IVehicleEngine>> GetEngineById(int id) => await _unitOfWork.EngineRepo.GetEngineById(id);

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByName(string type) => await _unitOfWork.EngineRepo.GetEngineByName(type);
}
