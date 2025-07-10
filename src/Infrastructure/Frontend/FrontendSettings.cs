using Contracts.Services.Frontend;

namespace Infrastructure.Frontend;

public class FrontendSettings : IFrontendSettings
{
    public const string SectionName = "FrontendSettings";

    public required string ConfirmEmailUrl { get; set; }
    public required string PasswordRecoveryUrl { get; set; }
}