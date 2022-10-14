using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;
using Lapka.Messages.Core.Entities;

namespace Lapka.Messages.Infrastructure.Mapper;

internal static class UserMapper
{
    public static UserDto AsDto(this AppUser user, string lastMessage)
        => new()
        {
            IsOnline = user.IsOnline,
            LastMessage = lastMessage,
        };
}