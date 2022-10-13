namespace Lapka.Messages.Application.Exceptions;

public class UnableToSendMessageException : ProjectException
{
    public UnableToSendMessageException() : base("Unable to send message")
    {
    }
}