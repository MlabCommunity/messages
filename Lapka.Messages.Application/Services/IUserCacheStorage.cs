namespace Lapka.Messages.Application.Services;

public interface IUserCacheStorage
{
    void SetShelterId(Guid principalId, Guid shelterId);
    Guid GetShelterId(Guid principalId);
    void SetRoomId(Guid principalId, Guid roomId);
    Guid GetRoomId(Guid principalId);
}