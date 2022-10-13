using Convey.CQRS.Queries;
using RoomDto = Lapka.Messages.Application.Dto.RoomDto;

namespace Lapka.Messages.Application.Queries;

public record GetAllRoomsQuery
    (Guid PrincipalId, int PageNumber = 1, int PageSize = 10) : IQuery<Dto.PagedResult<RoomDto>>;