namespace Lapka.Messages.Core.Repositories;

public interface IRoomRepository
{
    Task<Room> FindByUserIds(Guid user1, Guid user2);
    Task<Room> FindById(Guid roomId);
    Task CreateRoom(Room room);
}