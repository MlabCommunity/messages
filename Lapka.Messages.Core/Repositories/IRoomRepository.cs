using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Core.Repositories;

public interface IRoomRepository
{
    Task AddAsync(Room room);
    Task<Room> FindById(Guid roomId);

    Task<Room> FindByUsers(Guid principalId, Guid receiverId);
}