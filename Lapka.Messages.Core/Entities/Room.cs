namespace Lapka.Messages.Core;

public class Room
{
    public Guid RoomId { get; private set; }
    public List<AppUser> AppUsers = new List<AppUser>();
    public List<Message> Messages = new List<Message>();
    public int OnlineCount { get; private set; }

    private Room()
    {
    }

    public Room(AppUser user1, AppUser user2)
    {
        AppUsers.Add(user1);
        AppUsers.Add(user2);
        RoomId = Guid.NewGuid();
    }
}