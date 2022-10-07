namespace Lapka.Messages.Core;

public class Message
{
    public Guid MessageId { get; private set; }
    public Guid ReceiverId { get; private set; }
    public string SenderName { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsUnread { get; private set; } 
    public Guid RoomId { get; private set; }
    public Room Room { get; private set; }

    private Message()
    {
    }

    public Message(Guid receiverId, string content,string senderName,Room room)
    {
        Room = room;
        RoomId = room.RoomId;
        SenderName = senderName;
        CreatedAt = DateTime.UtcNow;
        MessageId = Guid.NewGuid();
        ReceiverId = receiverId;
        Content = content;
        IsUnread = true;
    }

    public void Read()
    {
        IsUnread = false;
    }
}