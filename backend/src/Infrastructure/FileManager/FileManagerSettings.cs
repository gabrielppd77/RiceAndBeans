using Contracts.Services.FileManager;

namespace Infrastructure.FileManager;

public class FileManagerSettings : IFileManagerSettings
{
    public const string SectionName = "FileManagerSettings";

    public required string Host { get; set; }
    public required string BaseUrl { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string MainBucket { get; set; }
    public required bool EnableSsl { get; set; }
}