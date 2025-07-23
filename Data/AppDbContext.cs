using Microsoft.EntityFrameworkCore;
using FieldManagementSystem.Models;

namespace FieldManagementSystem.Data;

public class AppDbContext : DbContext
{
    // AppDbContext is the main bridge between our C# code and the database.
    // It tells EF Core which models (tables) to manage and allows us to query or save data.

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<DeviceController> DeviceControllers { get; set; }
}
