using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DddModelsStore.DataAccess.Repositories;

public class ProductsRepo : IRepository<ProductEntity>
{
    private readonly MainDbContext _context;
    
    public ProductsRepo(MainDbContext context)
    {
        _context = context;     
    }
    public async Task AddAsync(ProductEntity entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        entity.Created = DateTime.Now;
        entity.Modified = DateTime.Now;
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProductEntity entity)
    {
        _context.Products.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var product = await _context.Products.SingleOrDefaultAsync(p=>p.Id==id);
        if (product == null)
            throw new ArgumentException("Product not found", nameof(id));
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductEntity>> GetAsync()
    {
        return _context.Products.AsNoTracking();
    }

    public async Task<ProductEntity> GetAsync(string id)
    {
        var product = await _context.Products.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id==id);
        return product ?? throw new ArgumentException("Product not found", nameof(id));
    }

    public async Task<bool> IsExistAsync(string id)
    {
        return await _context.Products.AnyAsync(p => p.Id==id);
    }
}