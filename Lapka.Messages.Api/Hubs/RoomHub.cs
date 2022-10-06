using System.Security.Claims;
using Convey.CQRS.Commands;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Core;
using Lapka.Pet.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Lapka.Messages.Api.Hubs;

[Authorize]
public class RoomHub : Hub
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IUserCacheStorage _storage;
    
    public RoomHub(ICommandDispatcher dispatcher, IUserCacheStorage storage)
    {
        _commandDispatcher = dispatcher;
        _storage = storage;
    }

    public override Task OnConnectedAsync()
    {
        var dupa = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        Groups.AddToGroupAsync(Context.ConnectionId, Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Groups.RemoveFromGroupAsync(Context.ConnectionId, Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(string content, string receiverId)
    {
        var stringPrincipalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        await Clients.Group(receiverId).SendAsync("ReceiveMessage", stringPrincipalId,content);

        var guidPrincipalId = Guid.Parse(stringPrincipalId);
        var guidReceiverId = Guid.Parse(receiverId); 
        
        var roomCommand = new EnsureRoomCommand(guidPrincipalId, guidReceiverId);
        await _commandDispatcher.SendAsync(roomCommand);

        var roomId = _storage.GetRoomId(guidPrincipalId);

        var messageCommand = new SendMessageCommand(guidPrincipalId,roomId, content);
        await _commandDispatcher.SendAsync(messageCommand);
    }
}