using Lapka.Pet.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Lapka.Pet.Infrastructure.CacheStorage;

public sealed class CacheStorage : ICacheStorage
{
    private readonly IMemoryCache _cache;

    public CacheStorage(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set<T>(string key, T value, TimeSpan? duration = null)
        => _cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5));

    public T Get<T>(string key) => _cache.Get<T>(key);
}