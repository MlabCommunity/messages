using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Consts;

namespace Lapka.Messages.Infrastructure.Mapper;

public static class MessageMapper
{
    public static MessageDto AsDto(this Message message,Guid senderId)
        => new()
        {
            Type = message.SenderId == senderId ? MessageType.SENT : MessageType.RECIVED,
            CreatedAt = message.CreatedAt,
            Content = message.Content
        };
}