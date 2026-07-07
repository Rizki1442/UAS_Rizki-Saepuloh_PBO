using Microsoft.EntityFrameworkCore;
using ParkingSystem.Models;

namespace ParkingSystem.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Officer> Officers => Set<Officer>();
    public DbSet<User> Users => Set<User>();
    public DbSet<ParkingTransaction> ParkingTransactions => Set<ParkingTransaction>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ParkingTransaction>()
            .HasOne(p => p.Vehicle)
            .WithMany()
            .HasForeignKey(p => p.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ParkingTransaction>()
            .HasOne(p => p.Officer)
            .WithMany()
            .HasForeignKey(p => p.OfficerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Username = "admin",
                Password = "admin123",
                FullName = "Administrator",
                Role = "Admin"
            }
        );
    }
}
