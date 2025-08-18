using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.Models.Common;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;

namespace Vehicle.Repository;

public class VehicleEngineRepository : IVehicleEngineRepository
{
    private readonly IVehicleDbContext _context;
    private readonly IMapper _mapper;

    public VehicleEngineRepository(IVehicleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IVehicleEngine>> GetEngineAsync()
    {
        var engine = await _context.VehicleEngine
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo< VehicleEngineDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return engine;
    }

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByIdAsync(int id)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Id == id)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleEngineDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return engine;
    }

    public async Task<IEnumerable<IVehicleEngine>> GetEngineByNameAsync(string type)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Type == type || e.Abrv == type)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleEngineDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return engine;
    }
}
