namespace Application.Common.Interfaces.Email.Templates;

public interface IPasswordRecoveryEmailTemplate
{
    string Generate(string recoveryLink);
}
