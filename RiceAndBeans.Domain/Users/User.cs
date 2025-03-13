namespace RiceAndBeans.Domain.Users;

public class User
{
    public User(string firstName, string lastName, string email, string password)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public Guid Id { get; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
}