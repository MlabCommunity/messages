using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Api.Hubs;
using Lapka.Messages.Api.Requests;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Application.Queries;
using Lapka.Pet.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;


namespace Lapka.Messages.Api;

public class MessageController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserCacheStorage _storage;
    private readonly IHubContext<RoomHub> _hub;

    public MessageController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher,
        IUserCacheStorage storage, IHubContext<RoomHub> hub)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _storage = storage;
        _hub = hub;
    }

    [Authorize]
    [HttpGet("{receiverId:guid}")]
    public async Task<ActionResult<PagedResult<MessageDto>>> Get(
        [FromRoute] Guid receiverId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetAllMessagesQuery(GetPrincipalId(), receiverId, pageNumber, pageSize);

        var result = await _queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SendMessageRequest request)
    {
        var principalId = GetPrincipalId();
        
        var roomCommand = new EnsureRoomCommand(principalId, request.ReceiverId);
        await _commandDispatcher.SendAsync(roomCommand);

        var roomId = _storage.GetRoomId(principalId);

        var messageCommand = new SendMessageCommand(principalId,roomId, request.Content);
        await _commandDispatcher.SendAsync(messageCommand);


        return NoContent();
    }
}