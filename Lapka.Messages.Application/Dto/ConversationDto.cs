namespace Lapka.Messages.Application.Dto;

public class ConversationDto
{
    public string? ProfilePhoto { get; set; }
    public string LastMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}