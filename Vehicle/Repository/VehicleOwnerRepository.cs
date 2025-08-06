using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Models.DTO;

namespace Vehicle.Repository;

public class VehicleOwnerRepository : IVehicleOwnerRepository
{
    private readonly VehicleDbContext _context;

    public VehicleOwnerRepository(VehicleDbContext context) => _context = context;

    public async Task<IEnumerable<IVehicleOwner>> GetOwners()
    {
        var owner = await _context.VehicleOwner
            .Select(o => new VehicleOwnerDTO { LastName = o.LastName, FirstName = o.FirstName, DOB = o.DOB })
            .ToListAsync();

        return owner;
    }

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerById(string name)
    {
        var owner = await _context.VehicleOwner
            .Where(o => o.FirstName == name || o.LastName == name)
            .Select(o => new VehicleOwnerDTO { LastName = o.LastName, FirstName = o.FirstName, DOB = o.DOB })
            .ToListAsync();

        return owner;
    }

    public async Task InsOwner(IVehicleOwner o)
    {
        if (o == null)
            return;

        var owner = new VehicleOwner()
        {
            LastName = o.LastName,
            FirstName = o.FirstName,
            DOB = o.DOB
        };

        _context.VehicleOwner.Add(owner);
        await _context.SaveChangesAsync();
    }

    public async Task DelOwner(int id)
    {
        var owner = await _context.VehicleOwner.FindAsync(id);

        if (owner == null)
            return;

        _context.VehicleOwner.Remove(owner);
        await _context.SaveChangesAsync();
    }

    public async Task UpdOwner(int id, IVehicleOwner UpdOwner)
    {
        var owner = await _context.VehicleOwner.FindAsync(id);
        if (owner == null)
            return;

        owner.FirstName= UpdOwner.FirstName;
        owner.LastName= UpdOwner.LastName;
        owner.DOB= UpdOwner.DOB;
        await _context.SaveChangesAsync();
    }
}
