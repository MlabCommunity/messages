namespace Lapka.Messages.Core;

public class AppUser
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? ProfilePicture { get; private set; }
    public bool IsOnline  {get;private set;}
    public List<Room> Rooms { get; private set; }


    private AppUser()
    {
    }

    public AppUser(Guid userId,string email, string firstName, string lastName,string profilePicture)
    {
        Email = email;
        ProfilePicture = profilePicture;
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        IsOnline = false;
    }

    public void Update(string firstName, string lastName, string email, string profilePicture)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ProfilePicture = profilePicture;
    }

    public void Online()
    {
        IsOnline = true;
    }

    public void Offline()
    {
        IsOnline = false;
    }
}