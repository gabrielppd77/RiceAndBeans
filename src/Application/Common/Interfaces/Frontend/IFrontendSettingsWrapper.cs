namespace Application.Common.Interfaces.Frontend;

public interface IFrontendSettingsWrapper
{
    public string ConfirmEmailUrl { get; }
    public string PasswordRecoveryUrl { get; }
}
