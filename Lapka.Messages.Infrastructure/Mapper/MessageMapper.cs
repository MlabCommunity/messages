using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Consts;
using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Infrastructure.Mapper;

internal static class MessageMapper
{
    public static MessageDto AsDto(this Message message, Guid principalId)
        => new()
        {
            Type = message.RoomId == principalId ? MessageType.SENT : MessageType.RECIVED,
            CreatedAt = message.CreatedAt,
            Content = message.Content
        };
}