using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Lapka.Messages.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;

namespace Lapka.Messages.Api;

public class MessageController : BaseController
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public MessageController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
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
}