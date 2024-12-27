using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.BusinessLogic.Models;
using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;

namespace DddModelsStore.BusinessLogic.Services;

public class CategoriesService : ICategoriesService
{
    private readonly IRepository<CategoryEntity> _categoryRepository;
    private readonly IMapper<Category, CategoryEntity> _mapper;
    
    public CategoriesService(IRepository<CategoryEntity> repository, 
        IMapper<Category, CategoryEntity> mapper)
    {
        _categoryRepository = repository;
        _mapper = mapper;
    }
    
    public async Task<Category> GetCategoryByIdAsync(string id)
    {
        var entity = await _categoryRepository.GetAsync(id);
        return await _mapper.MapAsync(entity);
    }

    public async Task<List<Category>> GetCategoriesListAsync()
    {
        var entities = await _categoryRepository.GetAsync();
        var models = new List<Category>();
        foreach (var entity in entities)
            models.Add(await _mapper.MapAsync(entity));
        return models; 
    }

    public async Task CreateCategoryAsync(string name)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        
        await _categoryRepository.AddAsync(
            new CategoryEntity(){ Name = name });
    }

    public async Task RenameCategoryAsync(string id, string name)
    {
        if(string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        if(string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        
        var entity = await _categoryRepository.GetAsync(id);
        entity.Name = name;
        await _categoryRepository.UpdateAsync(entity);
    }

    public Task DeleteCategoryAsync(string id)
    {
        return _categoryRepository.DeleteAsync(id);
    }
}