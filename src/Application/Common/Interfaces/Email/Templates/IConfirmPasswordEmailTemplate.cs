namespace Application.Common.Interfaces.Email.Templates;

public interface IConfirmPasswordEmailTemplate
{
    string Generate(string userName, string link);
}
