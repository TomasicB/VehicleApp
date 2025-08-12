using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;

namespace Vehicle.Repository.Common;
public interface IVehicleModelRepository
{
    Task<IEnumerable<VehicleModelDTO>> GetModels();

    Task<IEnumerable<VehicleModelDTO>> GetModelByName(string name);

    Task InsModel(VehicleModelDTO model, int makeid);

    Task DelModel(int id);

    Task UpdModel(int id, VehicleModelWriteDTO UpdModel);
}
