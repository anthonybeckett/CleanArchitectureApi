namespace CleanArchitectureApi.Application.Abstractions.Cache;

public interface ICachedQuery
{
    string CacheKey { get; }
    
    TimeSpan? Expiration { get; }
}