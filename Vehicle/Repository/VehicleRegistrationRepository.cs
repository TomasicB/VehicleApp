using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Repository.Common;
using Vehicle.Models.DTO;

namespace Vehicle.Repository;

public class VehicleRegistrationRepository : IVehicleRegistrationRepository
{
    private readonly VehicleDbContext _context;

    public VehicleRegistrationRepository(VehicleDbContext context) => _context = context;

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrations()
    {
        var registration = await _context.VehicleRegistration
            .Select(r => new VehicleRegistrationDTO
            {
                RegistrationNumber = r.RegistrationNumber,
                VehicleEngineId = r.VehicleEngineId,
                VehicleModelId = r.VehicleModelId,
                VehicleOwnerId = r.VehicleOwnerId
            })
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationById(string number)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.RegistrationNumber == number || r.RegistrationNumber == number)
            .Select(r => new VehicleRegistrationDTO
            {
                RegistrationNumber = r.RegistrationNumber,
                VehicleEngineId = r.VehicleEngineId,
                VehicleModelId = r.VehicleModelId,
                VehicleOwnerId = r.VehicleOwnerId
            })
            .ToListAsync();

        return registration;
    }

    public async Task InsRegistration(IVehicleRegistration r, int ModelId, int EngineId, int OwnerId)
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

        var registration = new VehicleRegistration()
        {
            RegistrationNumber = r.RegistrationNumber,
            VehicleModelId = model.Id,
            VehicleEngineId = engine.Id,
            VehicleOwnerId = owner.Id
        };

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

    public async Task UpdRegistration(int id, IVehicleRegistration UpdRegistration)
    {
        var registration = await _context.VehicleRegistration.FindAsync(id);
        if (registration == null)
            return;

        registration.RegistrationNumber = UpdRegistration.RegistrationNumber;
        registration.VehicleEngineId = UpdRegistration.VehicleEngineId;
        registration.VehicleModelId = UpdRegistration.VehicleModelId;
        registration.VehicleOwnerId = UpdRegistration.VehicleOwnerId;
        await _context.SaveChangesAsync();
    }
}
