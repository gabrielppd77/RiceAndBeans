namespace Contracts.Services.Frontend;

public interface IFrontendSettings
{
    public string ConfirmEmailUrl { get; }
    public string PasswordRecoveryUrl { get; }
}