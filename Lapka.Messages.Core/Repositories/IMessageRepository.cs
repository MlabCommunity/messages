namespace Lapka.Messages.Core.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
    Task<List<Message>> FindByRoomId(Guid roomId);
    Task UpdateAsync(List<Message> messages);
}