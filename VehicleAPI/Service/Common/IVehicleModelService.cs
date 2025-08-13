using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;

namespace Vehicle.Service.Common;
public interface IVehicleModelService
{
    Task<IEnumerable<IVehicleModel>> GetModels();

    Task<IEnumerable<IVehicleModel>> GetModelById(int id);

    Task<IEnumerable<IVehicleModel>> GetModelByName(string name);

    Task InsModel(IVehicleModel model, int makeid);

    Task DelModel(int id);

    Task UpdModel(int id, IVehicleModelWrite UpdModel);
}
