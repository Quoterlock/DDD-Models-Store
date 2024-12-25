namespace DddModelsStore.BusinessLogic.Interfaces;

public interface IMapper<TModel, TEntity>
{
    TModel Convet(TEntity entity);
    TEntity? Convet(TModel model);
}