using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Core.Repositories;

public interface IWorkerRepository
{
    Task AddAsync(Worker worker);
    Task<Worker> FindAsync(Guid id);
    Task DeleteAsync(Worker worker);
    Task DeleteAsync(List<Worker> workers);
    Task<bool> ExistsAsync(Guid id);
    Task<List<Worker>> FindByShelterId(Guid id);
}