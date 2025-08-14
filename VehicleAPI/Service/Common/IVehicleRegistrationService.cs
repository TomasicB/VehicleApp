using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;

namespace Vehicle.Service.Common;
public interface IVehicleRegistrationService
{
    Task<IEnumerable<IVehicleRegistration>> GetRegistrations();

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationById(int id);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumber(string number);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByEngine(string engine);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByModel(string model);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByOwner(string woner);

    Task InsRegistration(IVehicleRegistration registration, int ModelId, int EngineId, int OwnerId);
         
    Task DelRegistration(int id);
         
    Task UpdRegistration(int id, IVehicleRegistrationWrite UpdRegistration);
}
