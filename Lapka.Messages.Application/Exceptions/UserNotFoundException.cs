namespace Lapka.Messages.Application.Exceptions;

public class UserNotFoundException : ProjectException
{
    public UserNotFoundException() : base("User Not Found", 404)
    {
    }
}