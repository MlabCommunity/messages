using System.Reflection;
using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Application.Commands.Handlers;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Core;
using Lapka.Pet.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Messages.Api.Hubs;

[SignalRHub]
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

    public async Task SendMessage(string content, string receiverId)
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var command = new SendMessageCommand(Guid.Parse(principalId), Guid.Parse(receiverId), content);
        await _commandDispatcher.SendAsync(command);
        await Clients.Users(new List<string> { receiverId, principalId })
            .SendAsync("ReceiveMessage", principalId, content);
    }

    public async Task NotifyReceiveMessage(string senderId)
    {
        var command = new ReadMessagesCommand(
            Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Guid.Parse(senderId)
        );

        await _commandDispatcher.SendAsync(command);
    }

    public override async Task OnConnectedAsync()
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new UserOnlineCommand(Guid.Parse(principalId));
        await _commandDispatcher.SendAsync(command);

        
        await base.OnConnectedAsync();
        
        var query = new GetUnreadMessageCountQuery(Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)));
        var count = await _queryDispatcher.QueryAsync(query);
        
        _storage.SetUnreadMessageCount(principalId,count);
        
        var usersQuery = new GetAllUserConversationGuidQuery(Guid.Parse(principalId));
        var users = await _queryDispatcher.QueryAsync(usersQuery);

        await Clients.Users(users).SendAsync("Online", principalId, true);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new UserOfflineCommand(Guid.Parse(principalId));
        await _commandDispatcher.SendAsync(command);

        await base.OnDisconnectedAsync(exception);
        var query = new GetAllUserConversationGuidQuery(Guid.Parse(principalId));
        var users = await _queryDispatcher.QueryAsync(query);
        await Clients.Users(users).SendAsync("Online", principalId, false);
    }

    public async Task GetUnreadMessagesCount()
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var count = _storage.GetUnreadMessageCount(principalId);
        
        await Clients.User(Context.User.FindFirstValue(ClaimTypes.NameIdentifier))
            .SendAsync("MessageCount", count.ToString());
    }
}