namespace CleanArchitectureApi.Domain.Abstractions;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();

    TResult Query<TResult>(Func<IQueryable<TEntity>, TResult> query);

    Task<TResult> QueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> query);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);
}