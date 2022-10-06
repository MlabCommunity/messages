using Lapka.Messages.Infrastructure.Exceptions;

namespace Lapka.Messages.Application.Exceptions;

public class RoomNotFoundException : ProjectException
{
    public RoomNotFoundException() : base("Room not found", 404)
    {
    }
}