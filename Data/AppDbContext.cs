using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Models;

namespace FieldManagementSystem.Data;

/// <summary>
/// The application's database context, used by Entity Framework Core.
/// It acts as the main bridge between the code and the underlying database,
/// enabling data querying, updates, and persistence.
/// </summary>
public class AppDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Field>()
            .HasOne(f => f.User)
            .WithMany(u => u.Fields)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    /// <summary>
    /// Constructor for AppDbContext.
    /// Accepts options that configure the context (e.g., database provider, connection string).
    /// </summary>
    /// <param name="options">Options for configuring the DbContext.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// Represents the Users table in the database.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Represents the Fields table in the database.
    /// </summary>
    public DbSet<Field> Fields { get; set; }

    /// <summary>
    /// Represents the DeviceControllers table in the database.
    /// </summary>
    public DbSet<DeviceController> DeviceControllers { get; set; }
}
