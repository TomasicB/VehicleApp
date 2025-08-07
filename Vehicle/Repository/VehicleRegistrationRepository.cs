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
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .Select(r => new VehicleRegistrationDTO
            {
                RegistrationNumber = r.RegistrationNumber,
                VehicleEngine = r.VehicleEngine,
                VehicleModel = r.VehicleModel,
                VehicleOwner = r.VehicleOwner
            })
            .ToListAsync();

        return registration;
    }

    public async Task<IEnumerable<IVehicleRegistration>> GetRegistrationByNumber(string number)
    {
        var registration = await _context.VehicleRegistration
            .Where(r => r.RegistrationNumber == number || r.RegistrationNumber == number)
            .Include(vm => vm.VehicleModel)
            .Include(ve => ve.VehicleEngine)
            .Include(vo => vo.VehicleOwner)
            .Select(r => new VehicleRegistrationDTO
            {
                RegistrationNumber = r.RegistrationNumber,
                VehicleEngine = r.VehicleEngine,
                VehicleModel = r.VehicleModel,
                VehicleOwner = r.VehicleOwner
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
            VehicleModel = model,
            VehicleEngine = engine,
            VehicleOwner = owner
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
        registration.VehicleEngineId = UpdRegistration.VehicleEngine.Id;
        registration.VehicleModelId = UpdRegistration.VehicleModel.Id;
        registration.VehicleOwnerId = UpdRegistration.VehicleOwner.Id;
        await _context.SaveChangesAsync();
    }
}
