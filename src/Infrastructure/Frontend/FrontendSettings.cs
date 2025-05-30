namespace Infrastructure.Frontend;

public class FrontendSettings
{
    public const string SectionName = "FrontendSettings";

    public string ConfirmEmailUrl { get; set; } = "";
    public string PasswordRecoveryUrl { get; set; } = "";
}
