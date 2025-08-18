using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleRegistrationService : IVehicleRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleRegistrationService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsAsync() => await _unitOfWork.RegRepo.GetRegistrationsAsync();

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByIdAsync(int id) => await _unitOfWork.RegRepo.GetRegistrationByIdAsync(id);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumberAsync(string number) => await _unitOfWork.RegRepo.GetRegistrationByNumberAsync(number);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByEngineAsync(string engine) => await _unitOfWork.RegRepo.GetRegistrationsByEngineAsync(engine);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByModelAsync(string model) => await _unitOfWork.RegRepo.GetRegistrationsByModelAsync(model);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByOwnerAsync(string owner) => await _unitOfWork.RegRepo.GetRegistrationsByOwnerAsync(owner);

    public async Task InsertRegistrationAsync(IVehicleRegistration r, int ModelId, int EngineId, int OwnerId)
    {
        await _unitOfWork.RegRepo.InsertRegistrationAsync(r, ModelId, EngineId, OwnerId);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteRegistrationAsync(int id)
    {
        await _unitOfWork.RegRepo.DeleteRegistrationAsync(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdateRegistrationAsync(int id, IVehicleRegistrationWrite UpdRegistration)
    {
        await _unitOfWork.RegRepo.UpdateRegistrationAsync(id, UpdRegistration);
        await _unitOfWork.CommitAsync();
    }
}
