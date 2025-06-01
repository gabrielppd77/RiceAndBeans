namespace Domain.Users;

public class User
{
    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
    }

    public Guid Id { get; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsEmailConfirmed { get; private set; }
    public Guid? TokenEmailConfirmation { get; private set; }
    public Guid? TokenRecoverPassword { get; private set; }
    public DateTime? TokenRecoverPasswordExpire { get; private set; }

    public void StartEmailConfirmation()
    {
        IsEmailConfirmed = false;
        TokenEmailConfirmation = Guid.NewGuid();
    }

    public void ConfirmEmailConfirmation()
    {
        IsEmailConfirmed = true;
        TokenEmailConfirmation = null;
    }

    public void StartRecoverPassword()
    {
        TokenRecoverPassword = Guid.NewGuid();
        TokenRecoverPasswordExpire = DateTime.UtcNow.AddMinutes(10);
    }

    public void ResetRecoverPassword()
    {
        TokenRecoverPassword = null;
        TokenRecoverPasswordExpire = null;
    }
}