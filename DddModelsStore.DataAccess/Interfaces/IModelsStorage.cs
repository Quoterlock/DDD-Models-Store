namespace DddModelsStore.DataAccess.Interfaces;

public interface IModelsStorage
{
    Task<Stream> GetAsync(string id);
    Task StoreAsync(string id, Stream stream);
}