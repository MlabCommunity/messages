namespace Lapka.Messages.Application.Services;

public interface IUserCacheStorage
{
    void SetReceiverIds(Guid roomId, List<string> receiverIds);
    List<string> GetReceiverIds(Guid roomId);
    void SetRoomId(Guid principalId, Guid roomId);
    Guid GetRoomId(Guid principalId);
}