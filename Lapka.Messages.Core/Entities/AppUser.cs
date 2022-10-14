using Lapka.Messages.Core.Consts;

namespace Lapka.Messages.Core.Entities;

public class AppUser
{
    public Guid UserId { get; private set; }
    public Role Role { get; private set; }
    public bool IsOnline { get; private set; }
    public List<Room> Rooms { get; private set; }

    private AppUser()
    {
    }

    public AppUser(Guid userId,Role role)
    {
        UserId = userId;
        Role = role;
        IsOnline = false;
    }

    public void SetRole(Role role)
    {
        Role = role;
    }

    
    public void Online()
    {
        IsOnline = true;
    }

    public void Offline()
    {
        IsOnline = false;
    }
}