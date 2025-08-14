using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;

namespace Vehicle.Repository.Common;
public interface IVehicleRegistrationRepository
{
    Task<IEnumerable<IVehicleRegistration>> GetRegistrations();

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationById(int Id);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumber(string number);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByEngine(string engine);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByModel(string model);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByOwner(string owner);

    Task InsRegistration(IVehicleRegistration registration, int ModelId, int EngineId, int OwnerId);
         
    Task DelRegistration(int id);
         
    Task UpdRegistration(int id, IVehicleRegistrationWrite UpdRegistration);
}
