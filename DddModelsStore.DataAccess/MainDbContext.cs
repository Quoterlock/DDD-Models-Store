using DddModelsStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DddModelsStore.DataAccess;

public class MainDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ModelMetadataEntity> Models { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Title).IsRequired();
            // Owner is stored in separate db context, so it is not an FK 
            e.Property(p => p.OwnerId).IsRequired();
            e.Property(p=>p.Created).IsRequired();
            e.Property(p => p.Modified).IsRequired();
            // relation
            e.HasOne(p => p.Model);
            e.HasOne(p => p.Category)
                .WithMany(p=>p.Products)
                .HasForeignKey(p => p.CategoryId);
        });
        
        modelBuilder.Entity<ModelMetadataEntity>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p=>p.SizeBytes).IsRequired();
            e.Property(p=>p.Format).IsRequired();
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