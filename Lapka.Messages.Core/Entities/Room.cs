namespace Lapka.Messages.Core;

public class Room
{
    public Guid RoomId { get; private set; }
    public AppUser AppUser { get; private set; }
    public List<Message> Messages { get; private set; }

    private Room()
    {
    }

    public Room(Guid roomId,AppUser appUser, List<Message> messages)
    {
        AppUser = appUser;
        RoomId = roomId;
        Messages = messages;
    }
}