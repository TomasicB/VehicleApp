using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Entities;

namespace Vehicle.DAL.Context;

public class VehicleDbContext : DbContext, IVehicleDbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleOwner> VehicleOwner { get; set; }

    public DbSet<VehicleMake> VehicleMake { get; set; }

    public DbSet<VehicleModel> VehicleModel { get; set; }

    public DbSet<VehicleEngine> VehicleEngine { get; set; }

    public DbSet<VehicleRegistration> VehicleRegistration { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => base.SaveChangesAsync(cancellationToken);
}
