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
public class MessageHub : Hub
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public MessageHub(ICommandDispatcher dispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = dispatcher;
        _queryDispatcher = queryDispatcher;
    }

    public async Task SendMessage(string content, string receiverId)
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var command = new SendMessageCommand(Guid.Parse(principalId), Guid.Parse(receiverId), content);
        await _commandDispatcher.SendAsync(command);
        await Clients.Users(new List<string> { receiverId, principalId })
            .SendAsync("ReceiveMessage", principalId, content);

        // await NotifyReceiveMessage(receiverId);
    }

    public async Task NotifyReceiveMessage(string senderId)
    {
        var command = new ReadMessagesCommand(
            Guid.Parse(Context.User.FindFirstValue(ClaimTypes.NameIdentifier)),
            Guid.Parse(senderId)
        );

        await _commandDispatcher.SendAsync(command);
        await GetUnreadMessagesCount();
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var command = new UserOnlineCommand(Guid.Parse(principalId));
        await _commandDispatcher.SendAsync(command);

        var usersQuery = new GetAllOnlineUserIdQuery(Guid.Parse(principalId));
        var users = await _queryDispatcher.QueryAsync(usersQuery);

        await Clients.Users(users).SendAsync("Online", principalId, true);
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

    public async Task GetLastMessage(string receiverId)
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var query = new GetAllLastMessageQuery(
            Guid.Parse(principalId)
        );

        var result = await _queryDispatcher.QueryAsync(query);

        await Clients.User(principalId).SendAsync("LastMessage", result.Content,result.Type.ToString());
    }

    public async Task GetUnreadMessagesCount()
    {
        var principalId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new GetUnreadMessageCountQuery(Guid.Parse(principalId));

        var count = await _queryDispatcher.QueryAsync(query);
        await Clients.User(Context.User.FindFirstValue(ClaimTypes.NameIdentifier))
            .SendAsync("MessageCount", count.ToString());
    }
}