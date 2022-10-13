using Lapka.Messages.Core.Consts;

namespace Lapka.Messages.Application.Dto;

public class RoomDto
{
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public MessageDto? Message { get; set; }
    public int UnreadMessageCount { get; set; }
}