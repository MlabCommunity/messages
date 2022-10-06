namespace Lapka.Messages.Core.Repositories;

public interface IMessageRepository
{
    Task AddAsync(Message message);
}