using Lapka.Messages.Infrastructure.Exceptions;

namespace Lapka.Messages.Application.Exceptions;

public class UserNotFoundException : ProjectException
{
    public UserNotFoundException() : base("User Not Found", 404)
    {
    }
}