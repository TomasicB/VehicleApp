using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Vehicle.DAL.Context;
using Vehicle.DAL.Entities;
using Vehicle.Models.Common;
using Vehicle.Models.Common.Write;
using Vehicle.Models.DTOs;
using Vehicle.Repository.Common;

namespace Vehicle.Repository;

public class VehicleModelRepository : IVehicleModelRepository
{
    private readonly IVehicleDbContext _context; 
    private readonly IMapper _mapper;

    public VehicleModelRepository(IVehicleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<IVehicleModel>> GetModels()
    {
        var model = await _context.VehicleModel
            .Include(vm => vm.VehicleMake)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleModelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return model;
    }

    public async Task<IEnumerable<IVehicleModel>> GetModelById(int id)
    {
        var model = await _context.VehicleModel
            .Where(m => m.Id == id)
            .Include(vm => vm.VehicleMake)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleModelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return model;
    }

    public async Task<IEnumerable<IVehicleModel>> GetModelByName(string name)
    {
        var model = await _context.VehicleModel
            .Where(m => m.Name == name || m.Abrv == name)
            .Include(vm => vm.VehicleMake)
            .Include(vr => vr.VehicleRegistrations)
            .ProjectTo<VehicleModelDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return model;
    }

    public async Task InsModel(IVehicleModel m, int makeid)
    {
        if (m == null)
            return;

        var make = await _context.VehicleMake.FindAsync(makeid);
        if (make == null)
            return;

        var model = _mapper.Map<VehicleModel>(m);

        _context.VehicleModel.Add(model);
        await _context.SaveChangesAsync();
    }

    public async Task DelModel(int id)
    {
        var m = await _context.VehicleModel.FindAsync(id);

        if (m == null)
            return;

        _context.VehicleModel.Remove(m);
        await _context.SaveChangesAsync();
    }

    public async Task UpdModel(int id, IVehicleModelWrite UpdModel)
    {
        var model = await _context.VehicleModel.FindAsync(id);
        if (model == null)
            return;

        model.Name = UpdModel.Name;
        model.Abrv = UpdModel.Abrv;
        model.VehicleMakeId = UpdModel.VehicleMake.Id;
        await _context.SaveChangesAsync();
    }
}
