using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Application.Dto;
using Lapka.Messages.Application.Queries;
using Lapka.Messages.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lapka.Messages.Api.Controllers;

public class RoomController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IUserCacheStorage _storage;

    public RoomController(ICommandDispatcher commandDispatcher, IUserCacheStorage storage,
        IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _storage = storage;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("{receiverId:guid}")]
    [SwaggerOperation(summary: "Creates room")]
    [SwaggerResponse(200, "Room Created, returns roomId")]
    [SwaggerResponse(404,"User not found")]

    public async Task<IActionResult> Create([FromRoute] Guid receiverId)
    {
        var principalId = GetPrincipalId();

        var command = new CreateRoomCommand(principalId, receiverId);
        await _commandDispatcher.SendAsync(command);

        var roomId = _storage.GetRoomId(principalId);

        return Ok(roomId);
    }

    [HttpGet]
    [SwaggerOperation("Gets paged messages from specific room")]
    [SwaggerResponse(200, "Returns paged rooms",typeof(Application.Dto.PagedResult<RoomDto>))]
    public async Task<ActionResult<Application.Dto.PagedResult<MessageDto>>> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var principalId = GetPrincipalId();

        var query = new GetAllRoomsQuery(principalId, pageNumber, pageSize);

        var result = await _queryDispatcher.QueryAsync(query);

        return Ok(result);
    }

    [HttpGet("{receiverId:guid}")]
    [SwaggerOperation("Gets paged messages from specific room")]
    [SwaggerResponse(200, "Returns paged messages",typeof(Application.Dto.PagedResult<MessageDto>))]
    public async Task<ActionResult<Application.Dto.PagedResult<MessageDto>>> Get(
        [FromRoute] Guid receiverId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var principalId = GetPrincipalId();

        var command = new ReadMessagesCommand(principalId, receiverId);
        await _commandDispatcher.SendAsync(command);

        var query = new GetAllMessagesQuery(principalId, receiverId, pageNumber, pageSize);
        var result = await _queryDispatcher.QueryAsync(query);

        return Ok(result);
    }
}