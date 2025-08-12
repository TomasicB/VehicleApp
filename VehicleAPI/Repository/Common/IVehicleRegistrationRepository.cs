using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Repository.Common;
public interface IVehicleRegistrationRepository
{
    Task<IEnumerable<VehicleRegistrationDTO>> GetRegistrations();

    Task<IEnumerable<VehicleRegistrationDTO>> GetRegistrationByNumber(string number);

    Task InsRegistration(VehicleRegistrationDTO registration, int ModelId, int EngineId, int OwnerId);
         
    Task DelRegistration(int id);
         
    Task UpdRegistration(int id, VehicleRegistrationWriteDTO UpdRegistration);
}
