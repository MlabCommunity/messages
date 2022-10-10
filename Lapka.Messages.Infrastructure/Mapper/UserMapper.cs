using Lapka.Messages.Application.Dto;
using Lapka.Messages.Core;

namespace Lapka.Messages.Infrastructure.Mapper;

public static class UserMapper
{
    public static UserDto AsDto(this AppUser user, string lastMessage)
        => new()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsOnline = user.IsOnline,
            ProfilePhoto = user.ProfilePhoto,
            LastMessage = lastMessage,
        };
}