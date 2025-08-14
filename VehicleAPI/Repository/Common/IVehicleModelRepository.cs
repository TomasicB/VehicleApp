using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;

namespace Vehicle.Repository.Common;
public interface IVehicleModelRepository
{
    Task<IEnumerable<IVehicleModel>> GetModels();

    Task<IEnumerable<IVehicleModel>> GetModelById(int id);

    Task<IEnumerable<IVehicleModel>> GetModelByName(string name);

    Task<IEnumerable<IVehicleModel>> GetModelByMake(string make);

    Task InsModel(IVehicleModel model, int makeid);

    Task DelModel(int id);

    Task UpdModel(int id, IVehicleModelWrite UpdModel);
}
