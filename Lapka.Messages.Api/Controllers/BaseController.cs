using System.Security.Claims;
using Lapka.Messages.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Lapka.Messages.Api.Controllers;

[ApiController]
[Route("[controller]s")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
        => result is null ? NotFound() : Ok(result);

    protected Guid GetPrincipalId()
    {
        var stringId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(stringId))
        {
            throw new UserNotFoundException();
        }

        return Guid.Parse(stringId);
    }

    protected string GetPrincipalRole()
    {
        var role = User.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrEmpty(role))
        {
            throw new UserNotFoundException();
        }

        return role;
    }
}