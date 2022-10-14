using Lapka.Messages.Application.Services;

namespace Lapka.Messages.Infrastructure.Services;

internal sealed class UserCacheStorage : IUserCacheStorage
{
    private readonly ICacheStorage _cacheStorage;

    public UserCacheStorage(ICacheStorage cacheStorage)
    {
        _cacheStorage = cacheStorage;
    }

    public void SetShelterId(Guid principalId, Guid shelterId)
        => _cacheStorage.Set(principalId.ToString(), shelterId);

    public Guid GetShelterId(Guid principalId)
        => _cacheStorage.Get<Guid>(principalId.ToString());

    public void SetRoomId(Guid principalId, Guid roomId)
        => _cacheStorage.Set(principalId.ToString(), roomId);

    public Guid GetRoomId(Guid principalId)
        => _cacheStorage.Get<Guid>(principalId.ToString());
}