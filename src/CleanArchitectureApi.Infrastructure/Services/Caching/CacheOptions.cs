using Microsoft.Extensions.Caching.Distributed;

namespace CleanArchitectureApi.Infrastructure.Services.Caching;

public static class CacheOptions
{
    private static DistributedCacheEntryOptions DefaultExpiration =>
        new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) };
    
    public static DistributedCacheEntryOptions Create(TimeSpan? expiration) 
        => expiration != null
            ? new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expiration }
            : DefaultExpiration;
}