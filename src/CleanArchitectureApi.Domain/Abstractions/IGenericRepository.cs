namespace CleanArchitectureApi.Domain.Abstractions;

public interface IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    IQueryable<TEntity> GetAll();

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    TEntity Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void DeleteRange(IEnumerable<TEntity> entities);
}