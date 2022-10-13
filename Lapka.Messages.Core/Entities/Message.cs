namespace Lapka.Messages.Core.Entities;

public class Message
{
    public Guid SenderId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUnread { get; private set; }
    public Guid RoomId { get; private set; }
    public Room Room { get; private set; }

    private Message()
    {
    }

    public Message(Guid senderId, string content, Room room)
    {
        Room = room;
        RoomId = room.RoomId;
        CreatedAt = DateTime.UtcNow;
        SenderId = senderId;
        Content = content;
        IsUnread = true;
    }

    public void Read()
    {
        IsUnread = false;
    }
}