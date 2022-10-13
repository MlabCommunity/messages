namespace Lapka.Messages.Application.Exceptions;

public class UnableToCreateRoomException : ProjectException
{
    public UnableToCreateRoomException() : base("Unable to create room", 400)
    {
    }
}