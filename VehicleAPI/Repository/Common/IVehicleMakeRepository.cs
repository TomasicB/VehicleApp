using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Repository.Common;

public interface IVehicleMakeRepository
{
    Task<IEnumerable<VehicleMakeDTO>> GetMake();

    Task<IEnumerable<VehicleMakeDTO>> GetMakeByName(string name);

    Task InsMake(VehicleMakeDTO make);

    Task DelMake(int id);

    Task UpdMake(int id, VehicleMakeWriteDTO UpdMake);
}
