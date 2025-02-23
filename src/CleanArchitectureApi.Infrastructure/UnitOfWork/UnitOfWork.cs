using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork
{
    public async Task<string> CommitAsync(CancellationToken cancellationToken = default, bool checkConcurrency = false)
    {
        try
        {
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex) when (checkConcurrency)
        {
            return "A concurrency conflict occured while saving changes";
        }

        return string.Empty;
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        return new GenericRepository<TEntity>(applicationDbContext);
    }
}