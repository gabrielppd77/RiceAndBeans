namespace Domain.Users;

public class User
{
    public User(string firstName, string email, string password)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        Email = email;
        Password = password;
    }

    public Guid Id { get; }
	public string FirstName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}