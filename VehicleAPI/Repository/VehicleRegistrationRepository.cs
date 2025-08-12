using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.DTOs;
using Vehicle.Models.DTOs.Write;
using Vehicle.Repository.Common;

namespace Vehicle.Repository;

public class VehicleRegistrationRepository : IVehicleRegistrationRepository
{
    private readonly IVehicleDbContext _context;
    private readonly IMapper _mapper;

    public VehicleRegistrationRepository(IVehicleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VehicleRegistrationDTO>> GetRegistrations()
    {
        var registration = await _context.VehicleRegistration
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<VehicleRegistrationDTO>> GetRegistrationByNumber(string number)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.RegistrationNumber == number || r.RegistrationNumber == number)
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task InsRegistration(VehicleRegistrationDTO r, int ModelId, int EngineId, int OwnerId)
    {
        if (r == null)
            return;

        var model = await _context.VehicleModel.FindAsync(ModelId);
        if (model == null)
            return;

        var engine = await _context.VehicleEngine.FindAsync(EngineId);
        if (engine == null)
            return;

        var owner = await _context.VehicleOwner.FindAsync(OwnerId);
        if (owner == null)
            return;

        var registration = _mapper.Map<VehicleRegistration>(r);

        _context.VehicleRegistration.Add(registration);
        await _context.SaveChangesAsync();
    }

    public async Task DelRegistration(int id)
    {
        var registration = await _context.VehicleRegistration.FindAsync(id);

        if (registration == null)
            return;

        _context.VehicleRegistration.Remove(registration);
        await _context.SaveChangesAsync();
    }

    public async Task UpdRegistration(int id, VehicleRegistrationWriteDTO UpdRegistration)
    {
        var registration = await _context.VehicleRegistration.FindAsync(id);
        if (registration == null)
            return;

        registration.RegistrationNumber = UpdRegistration.RegistrationNumber;
        registration.VehicleEngineId = UpdRegistration.VehicleEngine.Id;
        registration.VehicleModelId = UpdRegistration.VehicleModel.Id;
        registration.VehicleOwnerId = UpdRegistration.VehicleOwner.Id;
        await _context.SaveChangesAsync();
    }
}
