using Microsoft.EntityFrameworkCore;
using SmartParkingApi.Models;

namespace SmartParkingApi.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

  public DbSet<Vehicle> Vehicles { get; set; }

  public DbSet<Car> Cars { get; set; }
  public DbSet<Van> Vans { get; set; }
  public DbSet<Motorbike> Motorbikes { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Config the Vehicle hierarchy using TPH (Table Per Hierarchy)
    modelBuilder.Entity<Vehicle>(entity =>
    {
      entity.HasKey(v => v.LicensePlate); // Use LicensePlate as the primary key

      entity.HasDiscriminator<string>("VehicleType")
      .HasValue<Car>("Car")
      .HasValue<Van>("Van")
      .HasValue<Motorbike>("Motorbike");
    });
  }

}