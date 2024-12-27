using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.BusinessLogic.Models;
using DddModelsStore.DataAccess.Entities;

namespace DddModelsStore.BusinessLogic.Services;

public class CategoryMapper : IMapper<Category, CategoryEntity>
{
    private readonly IMapper<Product, ProductEntity> _productMapper;
    
    public CategoryMapper(IMapper<Product, ProductEntity> productMapper)
    {
        _productMapper = productMapper; 
    }
    
    public async Task<Category> MapAsync(CategoryEntity entity)
    {
        var products = new List<Product>();
        foreach (var product in entity.Products)
            products.Add(await _productMapper.MapAsync(product));
        return new Category
        {
            Id = entity.Id,
            Name = entity.Name,
            Products = products
        };
    }

    public Task<CategoryEntity> MapAsync(Category model)
    {
        throw new NotImplementedException();
    }
}