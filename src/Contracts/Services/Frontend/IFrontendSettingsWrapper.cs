namespace Contracts.Services.Frontend;

public interface IFrontendSettingsWrapper
{
    public string ConfirmEmailUrl { get; }
    public string PasswordRecoveryUrl { get; }
}