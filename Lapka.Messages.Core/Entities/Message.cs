namespace Lapka.Messages.Core;

public class Message
{
    public Guid MessageId { get; private set; }
    public Guid SenderId { get; private set; }
    public string Content { get; private set; }
    public Room Room { get; private set; }

    private Message()
    {
    }

    public Message(Guid messageId, Guid senderId, string content,Room room)
    {
        MessageId = messageId;
        SenderId = senderId;
        Content = content;
        Room = room;
    }
}