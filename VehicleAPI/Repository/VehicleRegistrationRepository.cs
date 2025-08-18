using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
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

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsAsync()
    {
        var registration = await _context.VehicleRegistration
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByIdAsync(int id)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.Id == id)
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumberAsync(string number)
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

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByEngineAsync(string engine)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.VehicleEngine.Type == engine || r.VehicleEngine.Abrv == engine)
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByModelAsync(string model)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.VehicleModel.Name == model || r.VehicleModel.Abrv == model)
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationsByOwnerAsync(string owner)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.VehicleOwner.FirstName == owner || r.VehicleOwner.LastName == owner)
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .ProjectTo<VehicleRegistrationDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return registration;
    }

    public async Task InsertRegistrationAsync(IVehicleRegistration r, int ModelId, int EngineId, int OwnerId)
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

        await Task.CompletedTask;
    }

    public async Task DeleteRegistrationAsync(int id)
    {
        var registration = await _context.VehicleRegistration.FindAsync(id);

        if (registration == null)
            return;

        _context.VehicleRegistration.Remove(registration);

        await Task.CompletedTask;
    }

    public async Task UpdateRegistrationAsync(int id, IVehicleRegistrationWrite UpdRegistration)
    {
        var registration = await _context.VehicleRegistration.FindAsync(id);
        if (registration == null)
            return;

        registration.RegistrationNumber = UpdRegistration.RegistrationNumber;
        registration.VehicleEngineId = UpdRegistration.VehicleEngine.Id;
        registration.VehicleModelId = UpdRegistration.VehicleModel.Id;
        registration.VehicleOwnerId = UpdRegistration.VehicleOwner.Id;
        
        await Task.CompletedTask;
    }
}
