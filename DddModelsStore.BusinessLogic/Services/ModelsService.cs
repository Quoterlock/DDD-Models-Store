using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.DataAccess.Entities;
using DddModelsStore.DataAccess.Interfaces;

namespace DddModelsStore.BusinessLogic.Services;

public class ModelsService : IModelsService
{
    private readonly IRepository<ModelMetadataEntity> _metadata;
    private readonly IModelsStorage _storage;
    
    
    public ModelsService(IRepository<ModelMetadataEntity> _medatasRepository,
        IModelsStorage modelsStorage)
    {
        _metadata = _medatasRepository;
        _storage = modelsStorage;
    }
    
    public async Task<string> StoreModel(Stream dataStream)
    {
        throw new NotImplementedException();
        return Guid.NewGuid().ToString(); // TODO: implement models starage 
    }
}