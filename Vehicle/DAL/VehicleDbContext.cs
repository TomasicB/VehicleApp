using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Vehicle.Models;

namespace Vehicle.DAL;

public class VehicleDbContext : DbContext
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
}
