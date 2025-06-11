using Application.Common.Interfaces.Project.ApplyMigration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Project.ApplyMigration;

public class ApplyMigrationSettingsWrapper : IApplyMigrationSettingsWrapper
{
    private readonly ApplyMigrationSettings _applyMigrationSettings;

    public ApplyMigrationSettingsWrapper(IOptions<ApplyMigrationSettings> applyMigrationSettingsOptions)
    {
        _applyMigrationSettings = applyMigrationSettingsOptions.Value;
    }

    public string MigrationToken => _applyMigrationSettings.MigrationToken;
}