using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;

namespace Vehicle.Repository;

public class VehicleOwnerRepository : IVehicleOwnerRepository
{
    private readonly IVehicleDbContext _context;
    private readonly IMapper _mapper;

    public VehicleOwnerRepository(IVehicleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IVehicleOwner>> GetOwners()
    {
        var owner = await _context.VehicleOwner
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleOwnerDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return owner;
    }

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerById(int id)
    {
        var owner = await _context.VehicleOwner
            .Where(o => o.Id == id)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleOwnerDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return owner;
    }

    public async Task<IEnumerable<IVehicleOwner>> GetOwnerByName(string name)
    {
        var owner = await _context.VehicleOwner
            .Where(o => o.FirstName == name || o.LastName == name)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleOwnerDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return owner;
    }

    public async Task InsOwner(IVehicleOwner o)
    {
        if (o == null)
            return;

        var owner = _mapper.Map<VehicleOwner>(o);

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
