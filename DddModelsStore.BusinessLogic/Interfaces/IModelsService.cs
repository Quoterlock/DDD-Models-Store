using DddModelsStore.BusinessLogic.Models;

namespace DddModelsStore.BusinessLogic.Interfaces;

public interface IModelsService
{
    Task<string> StoreModel(Stream dataStream, string extension);
    Task<ModelFile> DownloadModel(string modelName);
}