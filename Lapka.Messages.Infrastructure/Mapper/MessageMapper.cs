using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Consts;

namespace Lapka.Messages.Infrastructure.Mapper;

public static class MessageMapper
{
    public static MessageDto AsDto(this Message message,Guid principalId)
        => new()
        {
            Type = message.SenderId == principalId ? MessageType.SENT : MessageType.RECIVED,
            Content = message.Content
        };
}