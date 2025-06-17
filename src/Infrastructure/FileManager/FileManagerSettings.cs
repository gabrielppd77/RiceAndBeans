namespace Infrastructure.FileManager;

public class FileManagerSettings
{
    public const string SectionName = "FileManagerSettings";

    public string BaseUrl { get; set; } = "";
    public string AccessKey { get; set; } = "";
    public string SecretKey { get; set; } = "";
    public string MainBucket { get; set; } = "";
}