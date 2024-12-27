using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.BusinessLogic.Models;
using DddModelsStore.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace DddModelsStore.BusinessLogic.Services;

public class ProductMapper : IMapper<Product, ProductEntity>
{
    private readonly IUserStore<AppUser> _users;
    
    public ProductMapper(IUserStore<AppUser> userStore)
    {
        _users = userStore;
    }
    
    public async Task<Product> MapAsync(ProductEntity entity)
    {
        return new Product()
        {
            Id = entity.Id,
            Description = entity.Description,
            Name = entity.Title,
            Modified = entity.Modified,
            Created = entity.Created,
            Category = new Category()
            {
                Id = entity.CategoryId,
                Name = entity.Category.Name
            },
            Owner = await _users.FindByIdAsync(entity.OwnerId, CancellationToken.None)
                    ?? throw new ArgumentException("User does not exist", nameof(entity.OwnerId)),
            Details = new ModelDetails()
            {
                ModelId = entity.Model.Id,
                SizeBytes = entity.Model.SizeBytes,
                TrianglesCount = entity.Model.TrianglesCount,
                TextureResolution = entity.Model.TextureResolution,
                Format = GetFormat(entity.Model.Format),
            }
        };
    }

    public async Task<ProductEntity> MapAsync(Product model)
    {
        throw new NotImplementedException();
    }
    
    private SupportedModelFormat GetFormat(string modelFormat)
    {
        if (modelFormat == "Obj")
            return SupportedModelFormat.Obj;
        if(modelFormat == "Fbx")
            return SupportedModelFormat.Fbx;
        throw new NotSupportedException($"Unsupported model format: {modelFormat}");
    }
}