using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DddModelsStore.DataAccess.Repositories;

public class CategoriesRepo : IRepository<CategoryEntity>
{
    private readonly MainDbContext _context;
    
    public CategoriesRepo(MainDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(CategoryEntity entity)
    {
        entity.Id = Guid.NewGuid().ToString();
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync(); 
    }

    public async Task UpdateAsync(CategoryEntity entity)
    {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        if (category == null)
            throw new ArgumentException($"Category with id: {id} was not found", nameof(id));
        
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CategoryEntity>> GetAsync()
    {
        return _context.Categories.AsNoTracking();
    }

    public async Task<CategoryEntity> GetAsync(string id)
    {
        var category = await _context.Categories.AsNoTracking()
            .Include(c=>c.Products)
            .SingleOrDefaultAsync(c=>c.Id==id);
        return category ??
            throw new ArgumentException($"Category with id: {id} was not found", nameof(id));
    }

    public async Task<bool> IsExistAsync(string id)
    {
        return await _context.Categories.AnyAsync(c=>c.Id==id);
    }
}