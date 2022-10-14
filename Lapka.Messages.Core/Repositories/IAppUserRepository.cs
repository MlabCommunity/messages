using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Core.Repositories;

public interface IAppUserRepository
{
    Task AddAsync(AppUser appUser);
    Task<bool> ExistAsync(Guid userId);
    Task<AppUser> FindAsync(Guid userId);
    Task UpdateAsync(AppUser appUser);
    Task DeleteAsync(AppUser appUser);
}