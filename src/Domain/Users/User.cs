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
    public Guid? TokenRecoverPassword { get; private set; }
    public DateTime? TokenRecoverPasswordExpire { get; private set; }

    public void CreateTokenRecoverPassword()
    {
        TokenRecoverPassword = Guid.NewGuid();
        TokenRecoverPasswordExpire = DateTime.UtcNow.AddMinutes(10);
    }
    
    public void RemoveTokenRecoverPassword()
    {
        TokenRecoverPassword = null;
        TokenRecoverPasswordExpire = null;
    }
}