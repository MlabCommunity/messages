namespace Lapka.Messages.Application.Exceptions;

public class RoomNotFoundException : ProjectException
{
    public RoomNotFoundException() : base("Room not found", 404)
    {
    }
}