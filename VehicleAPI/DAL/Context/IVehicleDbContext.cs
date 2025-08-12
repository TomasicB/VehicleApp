using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Vehicle.DAL.Entities;

namespace Vehicle.DAL.Context;

public interface IVehicleDbContext
{
    public DbSet<VehicleOwner> VehicleOwner { get; set; }

    public DbSet<VehicleMake> VehicleMake { get; set; }

    public DbSet<VehicleModel> VehicleModel { get; set; }

    public DbSet<VehicleEngine> VehicleEngine { get; set; }

    public DbSet<VehicleRegistration> VehicleRegistration { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
