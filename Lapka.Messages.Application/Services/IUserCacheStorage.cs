namespace Lapka.Pet.Application.Services;

public interface IUserCacheStorage
{
    void SetUnreadMessageCount(string principalId, int count);
    int GetUnreadMessageCount(string principalId);
}