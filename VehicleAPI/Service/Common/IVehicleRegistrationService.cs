using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;

namespace Vehicle.Service.Common;
public interface IVehicleRegistrationService
{
    Task<IEnumerable<IVehicleRegistration>> GetRegistrationsAsync();

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByIdAsync(int id);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumberAsync(string number);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByEngineAsync(string engine);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByModelAsync(string model);

    Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByOwnerAsync(string woner);

    Task InsertRegistrationAsync(IVehicleRegistration registration, int ModelId, int EngineId, int OwnerId);
         
    Task DeleteRegistrationAsync(int id);
         
    Task UpdateRegistrationAsync(int id, IVehicleRegistrationWrite UpdRegistration);
}
