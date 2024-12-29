using System.Text;
using DddModelsStore.BusinessLogic.Interfaces;
using DddModelsStore.BusinessLogic.Models;
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
    
    public async Task<string> StoreModel(Stream dataStream, string extension)
    {
        var id = Guid.NewGuid().ToString();
        
        var meta = new ModelMetadataEntity()
        {
            Id = id,
            Format = extension,
            SizeBytes = dataStream.Length,
            
            // add file analizer
            TrianglesCount = 0,
            TextureResolution = 0
        };
        
        await _storage.StoreAsync(id, dataStream);
        await _metadata.AddAsync(meta);

        return id;
    }

    public async Task<ModelFile> DownloadModel(string id)
    {
        if(string.IsNullOrEmpty(id))
            throw new ArgumentNullException(nameof(id));
        
        var meta = await _metadata.GetAsync(id);
        var stream = await _storage.GetAsync(id);
        return new ModelFile()
        {
            FileName = "model" + meta.Format,
            FileContent = stream,
        };
    }
}