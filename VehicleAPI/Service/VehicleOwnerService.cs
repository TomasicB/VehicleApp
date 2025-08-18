using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleOwnerService : IVehicleOwnerService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleOwnerService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleOwner>> GetOwnersAsync() => await _unitOfWork.OwnerRepo.GetOwnersAsync();

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerByIdAsync(int id) => await _unitOfWork.OwnerRepo.GetOwnerByIdAsync(id);

    public async Task<IEnumerable<IVehicleOwner>> GetOwnersByNameAsync(string name) => await _unitOfWork.OwnerRepo.GetOwnersByNameAsync(name);

    public async Task InsertOwnerAsync(IVehicleOwner o)
    {
        await _unitOfWork.OwnerRepo.InsertOwnerAsync(o);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteOwnerAsync(int id)
    {
        await _unitOfWork.OwnerRepo.DeleteOwnerAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateOwnerAsync(int id, IVehicleOwner UpdOwner)
    {
        await _unitOfWork.OwnerRepo.UpdateOwnerAsync(id, UpdOwner);
        await _unitOfWork.CommitAsync();
    }
}
