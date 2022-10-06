namespace Lapka.Messages.Core;

public class Message
{
    public Guid MessageId { get; private set; }
    public Guid SenderId { get; private set; }
    public string SenderName { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Room Room { get; private set; }

    private Message()
    {
    }

    public Message(Guid senderId, string content,string senderName,Room room)
    {
        Room = room;
        SenderName = senderName;
        CreatedAt = DateTime.UtcNow;
        MessageId = Guid.NewGuid();
        SenderId = senderId;
        Content = content;
    }
}