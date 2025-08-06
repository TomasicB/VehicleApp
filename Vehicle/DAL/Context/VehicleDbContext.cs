using Microsoft.EntityFrameworkCore;
using Vehicle.DAL.Entities;

namespace Vehicle.DAL.Context;

public class VehicleDbContext : DbContext
{
    public VehicleDbContext(DbContextOptions<VehicleDbContext> options)
        : base(options)
    {
    }

    public DbSet<VehicleOwnerDTO> VehicleOwner { get; set; }

    public DbSet<VehicleMakeDTO> VehicleMake { get; set; }

    public DbSet<VehicleModelDTO> VehicleModel { get; set; }

    public DbSet<VehicleEngine> VehicleEngine { get; set; }

    public DbSet<VehicleRegistrationDTO> VehicleRegistration { get; set; }
}
