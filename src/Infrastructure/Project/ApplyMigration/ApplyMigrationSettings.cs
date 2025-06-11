namespace Infrastructure.Project.ApplyMigration;

public class ApplyMigrationSettings
{
    public const string SectionName = "ApplyMigrationSettings";

    public string MigrationToken { get; set; } = "";
}