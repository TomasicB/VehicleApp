using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Vehicle.DAL.Context;
using Vehicle.Repository.Common;
using Vehicle.Models.DTOs;

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

    public async Task<IEnumerable<VehicleEngineDTO>> GetEngine()
    {
        var engine = await _context.VehicleEngine
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo< VehicleEngineDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return engine;
    }

    public async Task<IEnumerable<VehicleEngineDTO>> GetEngineById(int id)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Id == id)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleEngineDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return engine;
    }

    public async Task<IEnumerable<VehicleEngineDTO>> GetEngineByName(string type)
    {
        var engine = await _context.VehicleEngine
            .Where(e => e.Type == type || e.Abrv == type)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleEngineDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return engine;
    }
}
