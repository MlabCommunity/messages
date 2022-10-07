using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;

namespace Lapka.Messages.Infrastructure.Mapper;

public static class MessageMapper
{
    public static MessageDto AsDto(this Message message)
        => new()
        {
            SenderName = message.SenderName,
            SenderId = message.ReceiverId,
            Content = message.Content
        };
}