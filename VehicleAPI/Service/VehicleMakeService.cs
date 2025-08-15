using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleMakeService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleMake>> GetMake() => await _unitOfWork.MakeRepo.GetMake();

    public async Task<IEnumerable<IVehicleMake>> GetMakeById(int id) => await _unitOfWork.MakeRepo.GetMakeById(id);

    public async Task<IEnumerable<IVehicleMake>> GetMakeByName(string name) => await _unitOfWork.MakeRepo.GetMakeByName(name);

    public async Task InsMake(IVehicleMake m)
    {
        await _unitOfWork.MakeRepo.InsMake(m);
        await _unitOfWork.CommitAsync();
    }

    public async Task DelMake(int id)
    {
        await _unitOfWork.MakeRepo.DelMake(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdMake(int id, IVehicleMake UpdMake)
    {
        await _unitOfWork.MakeRepo.UpdMake(id, UpdMake);
        await _unitOfWork.CommitAsync();
    }
}
