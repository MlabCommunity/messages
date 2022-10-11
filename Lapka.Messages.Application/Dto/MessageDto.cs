using Lapka.Messages.Core.Consts;

namespace Lapka.Messages.Application.Dto;

public class MessageDto
{
    public MessageType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
}