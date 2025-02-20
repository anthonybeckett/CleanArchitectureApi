namespace CleanArchitectureApi.Domain.Abstractions;

public interface IUnitOfWork
{
    Task<string> CommitAsync(CancellationToken cancellationToken = default, bool checkConcurrency = false);
    
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
}