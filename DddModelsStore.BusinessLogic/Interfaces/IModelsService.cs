namespace DddModelsStore.BusinessLogic.Interfaces;

public interface IModelsService
{
    Task<string> StoreModel(Stream dataStream);
}