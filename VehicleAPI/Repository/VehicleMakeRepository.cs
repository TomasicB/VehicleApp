using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Repository.Common;

namespace Vehicle.Repository;

public class VehicleMakeRepository : IVehicleMakeRepository
{
    private readonly IVehicleDbContext _context;
    private readonly IMapper _mapper;

    public VehicleMakeRepository(IVehicleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VehicleMakeDTO>> GetMake()
    {
        var make = await _context.VehicleMake
            .Include(vm => vm.VehicleModels)
            .ProjectTo<VehicleMakeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return make;
    }

    public async Task<IEnumerable<VehicleMakeDTO>> GetMakeByName(string name)
    {
        var make = await _context.VehicleMake
            .Where(m => m.Name == name || m.Abrv == name)
            .Include(vm => vm.VehicleModels)
            .ProjectTo<VehicleMakeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return make;
    }

    public async Task InsMake(VehicleMakeDTO m)
    {
        if (m == null)
            return;

        var make = _mapper.Map<VehicleMake>(m);

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

    public async Task UpdMake(int id, VehicleMakeWriteDTO UpdMake)
    {
        var make = await _context.VehicleMake.FindAsync(id);
        if (make == null)
            return;

        make.Name = UpdMake.Name;
        make.Abrv = UpdMake.Abrv;
        await _context.SaveChangesAsync();
    }
}
