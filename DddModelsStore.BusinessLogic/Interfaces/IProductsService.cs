using DddModelsStore.BusinessLogic.Models;

namespace DddModelsStore.BusinessLogic.Interfaces;

public interface IProductsService
{
    Task AddProductAsync(NewProduct product);
    Task DeleteProductAsync(string id);
    Task<Product> GetProductByIdAsync(string id);
    Task<List<Product>> FindProductsAsync(string searchString);
}