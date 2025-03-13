using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using CleanArchitectureApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureApi.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext applicationDbContext) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default, bool checkConcurrency = false)
    {
        try
        {
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException) when (checkConcurrency)
        {
            throw new ConcurrencyException(["A concurrency conflict occured while saving changes."]);
        }
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        return new GenericRepository<TEntity>(applicationDbContext);
    }
}