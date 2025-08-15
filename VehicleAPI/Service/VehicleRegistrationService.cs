using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleRegistrationService : IVehicleRegistrationService
{
    private readonly IUnitOfWork _unitOfWork;

    public VehicleRegistrationService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrations() => await _unitOfWork.RegRepo.GetRegistrations();

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationById(int id) => await _unitOfWork.RegRepo.GetRegistrationById(id);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumber(string number) => await _unitOfWork.RegRepo.GetRegistrationByNumber(number);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByEngine(string engine) => await _unitOfWork.RegRepo.GetRegistrationByEngine(engine);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByModel(string model) => await _unitOfWork.RegRepo.GetRegistrationByModel(model);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByOwner(string owner) => await _unitOfWork.RegRepo.GetRegistrationByOwner(owner);

    public async Task InsRegistration(IVehicleRegistration r, int ModelId, int EngineId, int OwnerId)
    {
        await _unitOfWork.RegRepo.InsRegistration(r, ModelId, EngineId, OwnerId);
        await _unitOfWork.CommitAsync();
    }

    public async Task DelRegistration(int id)
    {
        await _unitOfWork.RegRepo.DelRegistration(id);
        await _unitOfWork.CommitAsync();
    }

    public async Task UpdRegistration(int id, IVehicleRegistrationWrite UpdRegistration)
    {
        await _unitOfWork.RegRepo.UpdRegistration(id, UpdRegistration);
        await _unitOfWork.CommitAsync();
    }
}
