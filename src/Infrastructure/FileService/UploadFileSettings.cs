namespace Infrastructure.FileService;

public class UploadFileSettings
{
    public const string SectionName = "UploadFileSettings";

    public string BaseUrl { get; set; } = "";
    public string AccessKey { get; set; } = "";
    public string SecretKey { get; set; } = "";
}
