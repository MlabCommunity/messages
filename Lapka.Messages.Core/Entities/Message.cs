namespace Lapka.Messages.Core;

public class Message
{
    public Guid MessageId { get; private set; }
    public Guid SenderId { get; private set; }
    public string SenderName { get; private set; }
    public string Content { get; private set; }
    public Room Room { get; private set; }

    private Message()
    {
    }

    public Message(Guid messageId, Guid senderId, string content,string senderName,Room room)
    {
        SenderName = senderName;
        MessageId = messageId;
        SenderId = senderId;
        Content = content;
        Room = room;
    }
}