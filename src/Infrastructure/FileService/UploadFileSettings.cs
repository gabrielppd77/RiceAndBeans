namespace Infrastructure.FileService;

public class UploadFileSettings
{
    public const string SectionName = "UploadFileSettings";

    public required string ServiceURL { get; set; }
    public required string AccessKey { get; set; }
    public required string SecretKey { get; set; }
}
