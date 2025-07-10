using Contracts.Services.Project.ApplyMigration;

namespace Infrastructure.Project.ApplyMigration;

public class ApplyMigrationSettings : IApplyMigrationSettings
{
    public const string SectionName = "ApplyMigrationSettings";

    public required string MigrationToken { get; set; }
}