namespace Infrastructure.Project.ApplyMigration;

public class ApplyMigrationSettings
{
    public const string SectionName = "ApplyMigrationSettings";

    public required string MigrationToken { get; set; }
}