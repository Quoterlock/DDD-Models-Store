namespace DddModelsStore.DataAccess.Interfaces;

public interface IRepository<TEntity>
{
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(string id);
    Task<IEnumerable<TEntity>> GetAsync();
    Task<TEntity> GetAsync(string id);
    Task<bool> IsExistAsync(string id);
}