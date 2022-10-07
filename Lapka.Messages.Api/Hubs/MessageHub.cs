using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Pet.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Lapka.Messages.Api.Hubs;

[Authorize]
public class RoomHub : Hub
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserCacheStorage _storage;

    public RoomHub(ICommandDispatcher dispatcher, IUserCacheStorage storage, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = dispatcher;
        _storage = storage;
        _queryDispatcher = queryDispatcher;
    }
    
    public async Task GetUnreadMessagesCount()
    {
        var principalId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var query = new GetUnreadMessageCountQuery(principalId);
        var result = await _queryDispatcher.QueryAsync(query);

        await Clients.User(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)).SendAsync("MessageCount", result.ToString());
    }

    public async Task SendMessage(string content, string roomId)
    {
        var stringPrincipalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var guidPrincipalId = Guid.Parse(stringPrincipalId);

        var guidRoomId = Guid.Parse(roomId);
        var messageCommand = new SendMessageCommand(guidPrincipalId, guidRoomId, content);
        await _commandDispatcher.SendAsync(messageCommand);
        await Clients.Group(roomId).SendAsync("ReceiveMessage", stringPrincipalId, content);
    }

    public async Task JoinRoom(string roomId)
    {
    }

    public async Task NotifyReceiveMessage(string roomId)
    {
        var command = new ReadMessagesCommand(
            Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Guid.Parse(roomId));

        await _commandDispatcher.SendAsync(command);
    }
}