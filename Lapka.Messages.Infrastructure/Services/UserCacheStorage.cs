using Lapka.Pet.Application.Services;

namespace Lapka.Pet.Infrastructure.CacheStorage;

internal sealed class UserCacheStorage : IUserCacheStorage
{
    private readonly ICacheStorage _cacheStorage;

    public UserCacheStorage(ICacheStorage cacheStorage)
    {
        _cacheStorage = cacheStorage;
    }
    
    public void SetRoomId(Guid principalId, Guid roomId)
        => _cacheStorage.Set(principalId.ToString(), roomId);

    public Guid GetRoomId(Guid principalId)
        => _cacheStorage.Get<Guid>(principalId.ToString());
}