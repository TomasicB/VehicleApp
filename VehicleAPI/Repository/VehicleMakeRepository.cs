using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.Common;
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

    public async Task<IEnumerable<IVehicleMake>> GetMakeAsync()
    {
        var make = await _context.VehicleMake
            .Include(vm => vm.VehicleModels)
            .ProjectTo<VehicleMakeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return make;
    }

    public async Task<IEnumerable<IVehicleMake>> GetMakeByIdAsync(int id)
    {
        var make = await _context.VehicleMake
            .Where(m => m.Id == id)
            .Include(vm => vm.VehicleModels)
            .ProjectTo<VehicleMakeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return make;
    }

    public async Task<IEnumerable<IVehicleMake>> GetMakesByNameAsync(string name)
    {
        var make = await _context.VehicleMake
            .Where(m => m.Name == name || m.Abrv == name)
            .Include(vm => vm.VehicleModels)
            .ProjectTo<VehicleMakeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return make;
    }

    public async Task InsertMakeAsync(IVehicleMake m)
    {
        if (m == null)
            return;

        var make = _mapper.Map<VehicleMake>(m);
        _context.VehicleMake.Add(make);

        await Task.CompletedTask;
    }

    public async Task DeleteMakeAsync(int id)
    {
        var make = await _context.VehicleMake.FindAsync(id);

        if (make == null)
            return;

        _context.VehicleMake.Remove(make);

        await Task.CompletedTask;
    }

    public async Task UpdateMakeAsync(int id, IVehicleMake UpdMake)
    {
        var make = await _context.VehicleMake.FindAsync(id);
        if (make == null)
            return;

        make.Name = UpdMake.Name;
        make.Abrv = UpdMake.Abrv;

        await Task.CompletedTask;
    }
}
