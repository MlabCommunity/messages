namespace Lapka.Messages.Core;

public class AppUser
{
    public Guid UserId { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string? ProfilePhoto { get; private set; }
    public bool IsOnline  {get;private set;}
    public List<Message> Messages { get; private set; }
    
    private AppUser()
    {
    }

    public AppUser(Guid userId,string email, string firstName, string lastName,string profilePhoto)
    {
        Email = email;
        ProfilePhoto = profilePhoto;
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
        ProfilePhoto = profilePicture;
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