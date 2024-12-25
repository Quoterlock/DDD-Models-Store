using DddModelsStore.BusinessLogic.Models;

namespace DddModelsStore.BusinessLogic.Interfaces;

public interface ICategoriesService
{
    Task<Category> GetCategoryByIdAsync(int id);
    Task<List<Category>> GetCategoriesListAsync();
    Task CreateCategoryAsync(string name);
    Task RenameCategoryAsync(string id, string name);
    Task DeleteCategoryAsync(string id);
}