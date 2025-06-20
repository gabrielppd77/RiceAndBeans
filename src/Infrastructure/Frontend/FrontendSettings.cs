namespace Infrastructure.Frontend;

public class FrontendSettings
{
    public const string SectionName = "FrontendSettings";

    public required string ConfirmEmailUrl { get; set; }
    public required string PasswordRecoveryUrl { get; set; }
}