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
using Swashbuckle.AspNetCore.Annotations;
using MessageDto = Lapka.Messages.Application.Dto.MessageDto;


namespace Lapka.Messages.Api;

[Authorize]
public class MessageController : BaseController
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public MessageController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet("{receiverId:guid}")]
    [SwaggerOperation("Gets paged messages from specific room")]
    public async Task<ActionResult<PagedResult<MessageDto>>> Get(
        [FromRoute] Guid receiverId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var principalId = GetPrincipalId();
        
        var command = new ReadMessagesCommand(principalId, receiverId);
        await _commandDispatcher.SendAsync(command);
        
        var query = new GetAllMessagesQuery(principalId,receiverId , pageNumber, pageSize);
        var result = await _queryDispatcher.QueryAsync(query);
        
        return Ok(result);
    }
    
}