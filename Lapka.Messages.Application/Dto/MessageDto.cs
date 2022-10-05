namespace Lapka.Messages.Application.Dto;

public class MessageDto
{
    public string SenderName { get; set; }
    public Guid SenderId { get; set; }
    public Guid MessageId { get; set; }
    public string Content { get; set; }
}