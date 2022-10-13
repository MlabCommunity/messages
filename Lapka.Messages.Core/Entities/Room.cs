namespace Lapka.Messages.Core.Entities;

public class Room
{
    public Guid RoomId { get; private set; }
    public List<AppUser> AppUsers { get; private set; }
    public List<Message> Messages { get; private set; }

    public Room()
    {
    }

    public Room(List<AppUser> appUsers)
    {
        RoomId = Guid.NewGuid();
        AppUsers = appUsers;
    }
}