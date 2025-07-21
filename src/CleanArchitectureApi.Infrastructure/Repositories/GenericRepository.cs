using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Infrastructure.Repositories;

public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public IQueryable<TEntity> GetAll()
    {
        return _context
            .Set<TEntity>()
            .AsQueryable();
    }

    public TResult Query<TResult>(Func<IQueryable<TEntity>, TResult> query)
        => query(GetAll());
    
    public Task<TResult> QueryAsync<TResult>(Func<IQueryable<TEntity>, Task<TResult>> query)
        => query(GetAll());

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context
            .Set<TEntity>()
            .FindAsync([id, cancellationToken], cancellationToken);
    }

    public async Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task CreateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
    }

    public TEntity Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);

        return entity;
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }
}