using DddModelsStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DddModelsStore.DataAccess;

public class MainDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Title).IsRequired();
            e.Property(p => p.OwnerId).IsRequired();
            e.Property(p=>p.Created).IsRequired();
            e.Property(p => p.Modified).IsRequired();
            // relations
            e.HasOne(p => p.Category)
                .WithMany(p=>p.Products)
                .HasForeignKey(p => p.CategoryId);
        });
        modelBuilder.Entity<CategoryEntity>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Name).IsRequired();
            // relations
            e.HasMany<ProductEntity>(p=>p.Products)
                .WithOne(p=>p.Category);
        });
        
        base.OnModelCreating(modelBuilder);
    }
}