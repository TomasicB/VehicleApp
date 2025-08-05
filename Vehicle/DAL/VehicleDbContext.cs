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
}
