
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Models.DTO;

namespace Vehicle.Repository;

public class VehicleModelRepository : IVehicleModelRepository
{
    private readonly VehicleDbContext _context;

    public VehicleModelRepository(VehicleDbContext context) => _context = context;

    public async Task<IEnumerable<IVehicleModel>> GetModels()
    {
        var model = await _context.VehicleModel
            .Select(m => new VehicleModelDTO { Name = m.Name, Abrv = m.Abrv, VehicleMakeId = m.VehicleMakeId })
            .ToListAsync();

        return model;
    }

    public async Task<IEnumerable<IVehicleModel>> GetModelById(string name)
    {
        var model = await _context.VehicleModel
            .Where(m => m.Name == name || m.Abrv == name)
            .Select(m => new VehicleModelDTO { Name = m.Name, Abrv = m.Abrv, VehicleMakeId = m.VehicleMakeId })
            .ToListAsync();

        return model;
    }

    public async Task InsModel(IVehicleModel m, int makeid)
    {
        if (m == null)
            return;

        var make = await _context.VehicleMake.FindAsync(makeid);
        if (make == null)
            return;

        var model = new VehicleModel()
        {
            Name = m.Name,
            Abrv = m.Abrv,
            VehicleMakeId = make.Id
        };

        _context.VehicleModel.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task DelModel(int id)
    {
        var m = await _context.VehicleModel.FindAsync(id);

        if (m == null)
            return;

        _context.VehicleModel.Remove(m);
        await _context.SaveChangesAsync();
    }

    public async Task UpdModel(int id, IVehicleModel UpdModel)
    {
        var model = await _context.VehicleModel.FindAsync(id);
        if (model == null)
            return;

        model.Name = UpdModel.Name;
        model.Abrv = UpdModel.Abrv;
        model.VehicleMakeId = UpdModel.VehicleMakeId;
        await _context.SaveChangesAsync();
    }
}
