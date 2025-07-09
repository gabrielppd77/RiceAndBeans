namespace Contracts.Services.Email.Templates;

public interface IPasswordRecoveryEmailTemplate
{
    string Generate(string recoveryLink);
}