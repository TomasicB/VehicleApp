using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Repository.Common;
using Vehicle.Service.Common;

namespace Vehicle.Service;

public class VehicleRegistrationService : IVehicleRegistrationService
{
    private readonly IVehicleRegistrationRepository _registrationRepo;

    public VehicleRegistrationService(IVehicleRegistrationRepository registrationRepo) => _registrationRepo = registrationRepo;

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrations() => await _registrationRepo.GetRegistrations();

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationById(int id) => await _registrationRepo.GetRegistrationById(id);

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumber(string number) => await _registrationRepo.GetRegistrationByNumber(number);

    public async Task InsRegistration(IVehicleRegistration r, int ModelId, int EngineId, int OwnerId) => await _registrationRepo.InsRegistration(r, ModelId, EngineId, OwnerId);

    public async Task DelRegistration(int id) => await _registrationRepo.DelRegistration(id);

    public async Task UpdRegistration(int id, IVehicleRegistrationWrite UpdRegistration) => await _registrationRepo.UpdRegistration(id, UpdRegistration);
}
