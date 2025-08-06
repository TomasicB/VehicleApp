using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Models.DTO;

namespace Vehicle.Repository;

public class VehicleMakeRepository : IVehicleMakeRepository
{
    private readonly VehicleDbContext _context;

    public VehicleMakeRepository(VehicleDbContext context) => _context = context;

    public async Task<IEnumerable<IVehicleMake>> GetMake()
    {
        var make = await _context.VehicleMake
            .Select(m => new VehicleMakeDTO { Name = m.Name, Abrv = m.Abrv})
            .ToListAsync();

        return make;
    }

    public async Task<IEnumerable<IVehicleMake>> GetMakeById(string name)
    {
        var make = await _context.VehicleMake
            .Where(m => m.Name == name || m.Abrv == name)
            .Select(m => new VehicleMakeDTO { Name = m.Name, Abrv = m.Abrv })
            .ToListAsync();

        return make;
    }

    public async Task InsMake(IVehicleMake m)
    {
        if (m == null)
            return;

        var make = new VehicleMake()
        {
            Name = m.Name, 
            Abrv = m.Abrv
        };

        _context.VehicleMake.Add(make);
        await _context.SaveChangesAsync();
    }

    public async Task DelMake(int id)
    {
        var make = await _context.VehicleMake.FindAsync(id);

        if (make == null)
            return;

        _context.VehicleMake.Remove(make);
        await _context.SaveChangesAsync();
    }

    public async Task UpdMake(int id, IVehicleMake UpdMake)
    {
        var make = await _context.VehicleMake.FindAsync(id);
        if (make == null)
            return;

        make.Name = UpdMake.Name;
        make.Abrv = UpdMake.Abrv;
        await _context.SaveChangesAsync();
    }
}
