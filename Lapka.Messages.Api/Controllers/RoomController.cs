using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Application.Commands;
using Lapka.Messages.Application.Dto;
using Lapka.Messages.Application.Queries;
using Lapka.Pet.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lapka.Messages.Api;

[Authorize]
public class RoomController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IUserCacheStorage _storage;

    public RoomController(ICommandDispatcher commandDispatcher, IUserCacheStorage storage)
    {
        _commandDispatcher = commandDispatcher;
        _storage = storage;
    }

    [HttpPost("{receiverId:guid}")]
    public async Task<ActionResult<int>> Create(
        [FromRoute] Guid receiverId)
    {
        var principalId = GetPrincipalId();
        var command = new CreateRoomCommand(principalId, receiverId);

        await _commandDispatcher.SendAsync(command);

        var roomId = _storage.GetRoomId(principalId);

        return Ok(roomId);
    }
}