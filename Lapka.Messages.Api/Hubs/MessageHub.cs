using System.Reflection;
using System.Security.Claims;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Application.Commands.Handlers;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Application.Services;
using Lapka.Messages.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Messages.Api.Hubs;

[SignalRHub]
[Authorize]
public class MessageHub : Hub
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserCacheStorage _storage;

    public MessageHub(ICommandDispatcher dispatcher, IQueryDispatcher queryDispatcher, IUserCacheStorage storage)
    {
        _commandDispatcher = dispatcher;
        _queryDispatcher = queryDispatcher;
        _storage = storage;
    }

    public async Task SendMessage(string content, string roomId)
    {
        var principalId = Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var command = new SendMessageCommand(principalId, Guid.Parse(roomId), content);
        await _commandDispatcher.SendAsync(command);
        
        var ids = _storage.GetReceiverIds(Guid.Parse(roomId));
        Console.WriteLine(ids.Count);
        await Clients.Users(ids)
            .SendAsync($"ReceiveMessage:{roomId}",principalId, content);
    }

    public async Task NotifyReceiveMessage(string roomId)
    {
        var command = new ReadMessagesCommand(
            Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Guid.Parse(roomId)
        );

        await _commandDispatcher.SendAsync(command);
        await GetUnreadMessagesCount();
    }

    
    public async Task GetUnreadMessagesCount()
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new GetUnreadMessageCountQuery(Guid.Parse(principalId));

        var count = await _queryDispatcher.QueryAsync(query);
        await Clients.User(Context.User.FindFirstValue(ClaimTypes.NameIdentifier))
            .SendAsync("MessageCount", count.ToString());
    }
    
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new UserOnlineCommand(Guid.Parse(principalId));
        await _commandDispatcher.SendAsync(command);

        var usersQuery = new GetAllOnlineUserIdQuery(Guid.Parse(principalId));
        var users = await _queryDispatcher.QueryAsync(usersQuery);
        
        if (users is not null)
        {
            await Clients.Users(users).SendAsync("Online", principalId, true);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);

        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new UserOfflineCommand(Guid.Parse(principalId));
        await _commandDispatcher.SendAsync(command);

        var query = new GetAllOnlineUserIdQuery(Guid.Parse(principalId));
        var users = await _queryDispatcher.QueryAsync(query);
        await Clients.Users(users).SendAsync("Online", principalId, false);
    }
    
}