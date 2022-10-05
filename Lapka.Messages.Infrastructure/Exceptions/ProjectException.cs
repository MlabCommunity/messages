namespace Lapka.Messages.Infrastructure.Exceptions;

public class ProjectException : Exception
{
    protected ProjectException(string message, int errorCode = 400) : base(message)
    {
        _errorCode = errorCode;
    }

    public int _errorCode { get; }
}