using Lapka.Messages.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Lapka.Messages.Infrastructure.Services;

public sealed class CacheStorage : ICacheStorage
{
    private readonly IMemoryCache _cache;

    public CacheStorage(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set<T>(string key, T value, TimeSpan? duration = null)
        => _cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5000));

    public T Get<T>(string key) => _cache.Get<T>(key);
}