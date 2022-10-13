namespace Lapka.Messages.Core.Entities;

public class AppUser
{
    public Guid UserId { get; private set; }
    public string Role { get; private set; }
    public bool IsOnline { get; private set; }
    public List<Room> Rooms { get; private set; }

    private AppUser()
    {
    }

    public AppUser(Guid userId, string email, string firstName, string lastName, string? profilePhoto)
    {
        UserId = userId;

        IsOnline = false;
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