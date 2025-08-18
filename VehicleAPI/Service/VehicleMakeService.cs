using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleMakeService : IVehicleMakeService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleMakeService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleMake>> GetMakeAsync() => await _unitOfWork.MakeRepo.GetMakeAsync();

    public async Task<IEnumerable<IVehicleMake>> GetMakeByIdAsync(int id) => await _unitOfWork.MakeRepo.GetMakeByIdAsync(id);

    public async Task<IEnumerable<IVehicleMake>> GetMakesByNameAsync(string name) => await _unitOfWork.MakeRepo.GetMakesByNameAsync(name);

    public async Task InsertMakeAsync(IVehicleMake m)
    {
        await _unitOfWork.MakeRepo.InsertMakeAsync(m);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteMakeAsync(int id)
    {
        await _unitOfWork.MakeRepo.DeleteMakeAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateMakeAsync(int id, IVehicleMake UpdMake)
    {
        await _unitOfWork.MakeRepo.UpdateMakeAsync(id, UpdMake);
        await _unitOfWork.CommitAsync();
    }
}
