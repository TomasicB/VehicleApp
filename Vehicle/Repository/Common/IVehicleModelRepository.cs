using Vehicle.Models.Common;

namespace Vehicle.Repository.Common;
public interface IVehicleModelRepository
{
    Task<IEnumerable<IVehicleModel>> GetModels();

    Task<IEnumerable<IVehicleModel>> GetModelByName(string name);

    Task InsModel(IVehicleModel model, int makeid);

    Task DelModel(int id);

    Task UpdModel(int id, IVehicleModel UpdModel);
}
