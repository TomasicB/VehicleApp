using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleOwnerService : IVehicleOwnerService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleOwnerService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleOwner>> GetOwners() => await _unitOfWork.OwnerRepo.GetOwners();

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerById(int id) => await _unitOfWork.OwnerRepo.GetOwnerById(id);

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerByName(string name) => await _unitOfWork.OwnerRepo.GetOwnerByName(name);

    public async Task InsOwner(IVehicleOwner o)
    {
        await _unitOfWork.OwnerRepo.InsOwner(o);
        await _unitOfWork.CommitAsync();
    }

    public async Task DelOwner(int id)
    {
        await _unitOfWork.OwnerRepo.DelOwner(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdOwner(int id, IVehicleOwner UpdOwner)
    {
        await _unitOfWork.OwnerRepo.UpdOwner(id, UpdOwner);
        await _unitOfWork.CommitAsync();
    }
}
