using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;

namespace Vehicle.Repository.Common;
public interface IVehicleModelRepository
{
    Task<IEnumerable<IVehicleModel>> GetModelsAsync();

    Task<IEnumerable<IVehicleModel>> GetModelByIdAsync(int id);

    Task<IEnumerable<IVehicleModel>> GetModelsByNameAsync(string name);

    Task<IEnumerable<IVehicleModel>> GetModelsByMakeAsync(string make);

    Task InsertModelAsync(IVehicleModel model, int makeid);

    Task DeleteModelAsync(int id);

    Task UpdateModelAsync(int id, IVehicleModelWrite UpdModel);
}
