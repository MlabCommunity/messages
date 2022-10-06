namespace Lapka.Pet.Application.Services;

public interface IUserCacheStorage
{
    void SetRoomId(Guid principalId, Guid roomId);


    Guid GetRoomId(Guid principalId);
}