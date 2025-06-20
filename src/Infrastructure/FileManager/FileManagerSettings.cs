namespace Infrastructure.FileManager;

public class FileManagerSettings
{
    public const string SectionName = "FileManagerSettings";

    public required string BaseUrl { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
    public required string MainBucket { get; set; }
}