using Lapka.Messages.Application.Services;

namespace Lapka.Messages.Infrastructure.Services;

internal sealed class UserCacheStorage : IUserCacheStorage
{
    private readonly ICacheStorage _cacheStorage;

    public UserCacheStorage(ICacheStorage cacheStorage)
    {
        _cacheStorage = cacheStorage;
    }

    public void SetReceiverIds(Guid roomId,List<string> receiverIds)
        => _cacheStorage.Set(roomId.ToString(), receiverIds);

    public List<string> GetReceiverIds(Guid roomId)
        => _cacheStorage.Get<List<string>>(roomId.ToString());

    public void SetRoomId(Guid principalId, Guid roomId)
        => _cacheStorage.Set(principalId.ToString(), roomId);

    public Guid GetRoomId(Guid principalId)
        => _cacheStorage.Get<Guid>(principalId.ToString());
}