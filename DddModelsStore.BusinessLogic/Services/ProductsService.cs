using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.BusinessLogic.Models;
using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DddModelsStore.BusinessLogic.Services;

public class ProductsService : IProductsService
{
    private readonly IRepository<ProductEntity> _products;
    private readonly IModelsService _models;
    private readonly IMapper<Product, ProductEntity> _mapper;
    
    public ProductsService(IRepository<ProductEntity> productsRepository,
        IModelsService modelsService,
        IMapper<Product, ProductEntity> productsMapper)
    {
        _products = productsRepository;
        _models = modelsService;
        _mapper = productsMapper;
    }
    
    public async Task AddProductAsync(NewProduct product)
    {
        // add model metadata
        var modelId = await _models.StoreModel(product.ModelData);
        // add product
        _products.AddAsync(new ProductEntity()
        {
            CategoryId = product.CategoryId,
            Description = product.Description,
            Title = product.Title,
            OwnerId = product.Owner.Id,
            Model = new ModelMetadataEntity(){ Id = modelId }
        });
    }

    public async Task DeleteProductAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        await _products.DeleteAsync(id);
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        var entity = await _products.GetAsync(id);
        if(entity == null)
            throw new ArgumentException("Product not found", nameof(id));
        return await _mapper.MapAsync(entity);
    }


    public async Task<List<Product>> FindProductsAsync(string searchString)
    {
        var entities = (await _products.GetAsync())
            .Where(p => p.Title.Contains(searchString) 
                        || p.Description.Contains(searchString));
        var models = new List<Product>();
        foreach (var entity in entities)
            models.Add(await _mapper.MapAsync(entity)); 
        return models;
    }
}