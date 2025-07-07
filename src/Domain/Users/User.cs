using Domain.Companies;

namespace Domain.Users;

public class User
{
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? UrlImage { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public Guid? TokenEmailConfirmation { get; private set; }
    public Guid? TokenRecoverPassword { get; private set; }
    public DateTime? TokenRecoverPasswordExpire { get; private set; }
    public Company? Company { get; private set; }

    protected User()
    {
    }

    public User(string name, string email, string password)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;

        IsEmailConfirmed = false;
        TokenEmailConfirmation = Guid.NewGuid();
    }

    public void SetCompany(Company company)
    {
        Company = company;
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
        TokenEmailConfirmation = null;
    }

    public void StartRecoverPassword()
    {
        TokenRecoverPassword = Guid.NewGuid();
        TokenRecoverPasswordExpire = DateTime.UtcNow.AddMinutes(10);
    }

    public void ResetRecoverPassword(string newPassword)
    {
        TokenRecoverPassword = null;
        TokenRecoverPasswordExpire = null;
        Password = newPassword;
    }

    public void UpdateImage(string? urlImage)
    {
        UrlImage = urlImage;
    }

    public void UpdateFormFields(string name)
    {
        Name = name;
    }
}