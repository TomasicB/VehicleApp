using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleEngineService : IVehicleEngineService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleEngineService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleEngine>> GetEngineAsync() => await _unitOfWork.EngineRepo.GetEngineAsync();

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByIdAsync(int id) => await _unitOfWork.EngineRepo.GetEngineByIdAsync(id);

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByNameAsync(string type) => await _unitOfWork.EngineRepo.GetEngineByNameAsync(type);
}
