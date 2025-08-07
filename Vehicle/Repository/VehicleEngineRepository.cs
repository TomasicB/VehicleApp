using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Models.DTO;

namespace Vehicle.Repository;

public class VehicleEngineRepository : IVehicleEngineRepository
{
    private readonly VehicleDbContext _context;

    public VehicleEngineRepository(VehicleDbContext context) => _context = context;

    public async Task<IEnumerable<IVehicleEngine>> GetEngine()
    {
        var engine = await _context.VehicleEngine
            .Include(vr => vr.VehicleRegistrations)
            .Select(e=> new VehicleEngineDTO { Type = e.Type, Abrv = e.Abrv })
            .ToListAsync();

        return engine;
    }

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByName(string type)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Type == type || e.Abrv == type)
            .Include(vr => vr.VehicleRegistrations)
            .Select(e => new VehicleEngineDTO { Type = e.Type, Abrv = e.Abrv })
            .ToListAsync();

        return engine;
    }

    public async Task InsEngine(IVehicleEngine e)
    {
        if (e == null)
            return;

        var engine = new VehicleEngine()
        {
            Type = e.Type,
            Abrv = e.Abrv
        };

        _context.VehicleEngine.Add(engine);
        await _context.SaveChangesAsync();
    }

    public async Task DelEngine(int id)
    {
        var engine = await _context.VehicleEngine.FindAsync(id);

        if (engine == null)
            return;

        _context.VehicleEngine.Remove(engine);
        await _context.SaveChangesAsync();
    }

    public async Task UpdEngine(int id, IVehicleEngine UpdEngine)
    {
        var engine = await _context.VehicleEngine.FindAsync(id);
        if (engine == null)
            return;

        engine.Type = UpdEngine.Type;
        engine.Abrv = UpdEngine.Abrv;
        await _context.SaveChangesAsync();
    }
}
