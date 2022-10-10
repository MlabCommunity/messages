using Convey.CQRS.Queries;
using Lapka.Messages.Application.Dto;

namespace Lapka.Messages.Api;

public class UserController : BaseController
{
    private readonly IQueryDispatcher _queryDispatcher;

    public UserController(IQueryDispatcher queryDispatcher)
    {
        _queryDispatcher = queryDispatcher;
    }
    

}