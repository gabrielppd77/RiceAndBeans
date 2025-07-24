namespace Contracts.Services.FileManager;

public interface IFileManagerSettings
{
    string BaseUrl { get; set; }
    string MainBucket { get; set; }
}