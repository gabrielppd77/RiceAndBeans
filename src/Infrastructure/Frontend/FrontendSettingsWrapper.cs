using Microsoft.Extensions.Options;
using Contracts.Services.Frontend;

namespace Infrastructure.Frontend;

public class FrontendSettingsWrapper : IFrontendSettingsWrapper
{
    private readonly FrontendSettings _frontendSettings;

    public FrontendSettingsWrapper(IOptions<FrontendSettings> _frontendSettingsOptions)
    {
        _frontendSettings = _frontendSettingsOptions.Value;
    }

    public string ConfirmEmailUrl => _frontendSettings.ConfirmEmailUrl;
    public string PasswordRecoveryUrl => _frontendSettings.PasswordRecoveryUrl;
}