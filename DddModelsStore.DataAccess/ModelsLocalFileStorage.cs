using DddModelsStore.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DddModelsStore.DataAccess;

public class ModelsLocalFileStorage : IModelsStorage
{
    private readonly string _root;

    ModelsLocalFileStorage(IConfiguration configuration)
    {
        _root = configuration.GetSection("ModelsStorage:RootPath").Value ??
                throw new ArgumentException("Root is not defined in configuration");
    }


    public async Task<Stream> GetAsync(string id)
    {
        var path = Path.Combine(_root, id + ".stored_model");
        if (!File.Exists(path))
            throw new ArgumentException("File not found", nameof(id));
        
        var stream = new FileStream(path, FileMode.Open);
        return stream; 
    }

    public async Task StoreAsync(string id, Stream stream)
    {
        if (!Directory.Exists(_root))
            throw new ArgumentException("Root directory is not exists", nameof(_root));
        
        var path = Path.Combine(_root, id + ".stored_model");
        var fileStream = new FileStream(path, FileMode.Create);
        await stream.CopyToAsync(fileStream);
        fileStream.Close();
        stream.Close();
    }
}