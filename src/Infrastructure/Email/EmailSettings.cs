namespace Infrastructure.Email;

public class EmailSettings
{
    public const string SectionName = "EmailSettings";

    public required string From { get; set; }
    public required string DisplayName { get; set; }
    public required string Host { get; set; }
    public required int Port { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required bool EnableSsl { get; set; }
}