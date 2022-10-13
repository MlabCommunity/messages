using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Core.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task UpdateAsync(List<Message> messages);
    Task<List<Message>> FindUnreadByRoomId(Guid roomId,Guid principalId);
}