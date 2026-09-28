namespace Contracts.Services.Email.Templates;

public interface IConfirmPasswordEmailTemplate
{
    string Generate(string userName, string link);
}