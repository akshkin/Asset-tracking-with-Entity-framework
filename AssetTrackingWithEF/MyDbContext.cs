using AssetTrackingWithEF.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;

namespace AssetTrackingWithEF;

public class MyDbContext : DbContext
{
    private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Assets;Trusted_Connection=True;";

    public DbSet<Category> Categories { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Asset> Assets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // We tell the app to use the connectionstring.
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {        // Seed Offices
        modelBuilder.Entity<Office>().HasData(
            new Office { OfficeId = 1, OfficeLocation = "New York", CurrencyCode = "USD", ConversionRateFromUSD = 1 },
            new Office { OfficeId = 2, OfficeLocation = "London", CurrencyCode = "GBP", ConversionRateFromUSD = 0.79m },
            new Office { OfficeId = 3, OfficeLocation = "Tokyo", CurrencyCode = "JPY", ConversionRateFromUSD = 150 }
        );

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, CategoryName = "Laptop" },
            new Category { CategoryId = 2, CategoryName = "Phone" }
        );

    }

}
