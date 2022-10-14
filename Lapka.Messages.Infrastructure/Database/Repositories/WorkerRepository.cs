using Lapka.Messages.Core.Entities;
using Lapka.Messages.Core.Repositories;
using Lapka.Messages.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lapka.Messages.Infrastructure.Database.Repositories;

internal sealed class WorkerRepository : IWorkerRepository
{
    private readonly DbSet<Worker> _workers;
    private readonly AppDbContext _context;

    public WorkerRepository(AppDbContext context)
    {
        _workers = context.Workers;
        _context = context;
    }

    public async Task AddAsync(Worker worker)
    {
        await _workers.AddAsync(worker);
        await _context.SaveChangesAsync();
    }

    public Task<Worker> FindAsync(Guid id)
        => _workers
            .FirstOrDefaultAsync(x => x.WorkerId == id);

    public async Task DeleteAsync(Worker worker)
    {
        _workers.Remove(worker);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(List<Worker> workers)
    {
        _workers.RemoveRange(workers);
        await _context.SaveChangesAsync();
    }

    public Task<bool> ExistsAsync(Guid id)
        => _workers.AnyAsync(x => x.WorkerId == id);

    public Task<List<Worker>> FindByShelterId(Guid id)
        => _workers
            .Where(x => x.ShelterId == id)
            .ToListAsync();
}