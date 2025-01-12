using FastCost.DAL;
using FastCost.DAL.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public const string DatabaseFilename = "FastCostSQLite.db3";
    public static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    public DbSet<Cost> Costs { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}

