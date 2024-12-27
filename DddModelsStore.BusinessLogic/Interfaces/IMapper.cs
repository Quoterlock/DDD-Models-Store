namespace DddModelsStore.BusinessLogic.Interfaces;

public interface IMapper<TModel, TEntity>
{
    Task<TModel> MapAsync(TEntity entity);
    Task<TEntity> MapAsync(TModel model);
    
}