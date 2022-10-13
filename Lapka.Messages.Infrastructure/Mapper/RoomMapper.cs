using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Consts;
using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Infrastructure.Mapper;

internal static class RoomMapper
{
    public static RoomDto AsDto(this Room room, Guid principalId)
        => new()
        {
            RoomId = room.RoomId,
            UserId = room.AppUsers.FirstOrDefault(x => x.UserId != principalId).UserId,
            Message = room.Messages?.MaxBy(x => x.CreatedAt)?.AsDto(principalId),
            UnreadMessageCount = room.Messages.Count(x => x.IsUnread && x.SenderId != principalId)
        };
}