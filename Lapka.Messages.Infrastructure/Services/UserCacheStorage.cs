using Lapka.Pet.Application.Services;

namespace Lapka.Pet.Infrastructure.CacheStorage;

internal sealed class UserCacheStorage : IUserCacheStorage
{
    private readonly ICacheStorage _cacheStorage;

    public UserCacheStorage(ICacheStorage cacheStorage)
    {
        _cacheStorage = cacheStorage;
    }
    public void SetUnreadMessageCount(string principalId, int count)
        => _cacheStorage.Set(principalId.ToString(), count);

    public int GetUnreadMessageCount(string principalId)
        => _cacheStorage.Get<int>(principalId.ToString());
}