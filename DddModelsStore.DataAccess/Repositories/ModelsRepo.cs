using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DddModelsStore.DataAccess.Repositories;

public class ModelsRepo : IRepository<ModelMetadataEntity>
{
    private readonly MainDbContext _context;

    public ModelsRepo(MainDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(ModelMetadataEntity entity)
    {
        _context.Models.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ModelMetadataEntity entity)
    {
        _context.Models.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var model = await _context.Models.SingleOrDefaultAsync(m=>m.Id==id);
        if (model == null)
            throw new ArgumentException("Model not found", nameof(id));
        _context.Models.Remove(model);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ModelMetadataEntity>> GetAsync()
    {
        return _context.Models;
    }

    public async Task<ModelMetadataEntity> GetAsync(string id)
    {
        var result = await _context.Models.SingleOrDefaultAsync(m => m.Id == id);
        if(result == null)
            throw new ArgumentException("Model not found", nameof(id));
        return result;
    }

    public async Task<bool> IsExistAsync(string id)
    {
        return await _context.Models.AnyAsync(m => m.Id == id);
    }
}