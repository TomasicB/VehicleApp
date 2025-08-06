using Vehicle.Models.Common;

namespace Vehicle.Repository.Common;
public interface IVehicleRegistrationRepository
{
    Task<IEnumerable<IVehicleRegistration>> GetRegistrations();

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationById(string number);

    Task InsRegistration(IVehicleRegistration registration, int ModelId, int EngineId, int OwnerId);
         
    Task DelRegistration(int id);
         
    Task UpdRegistration(int id, IVehicleRegistration UpdRegistration);
}
