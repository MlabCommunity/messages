namespace Lapka.Messages.Application.Dto;

public class UserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsOnline { get; set; }
    public string? ProfilePhoto { get; set; }
    public string LastMessage { get; set; }
    
}