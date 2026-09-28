using ErrorOr;

namespace Contracts.Services.FileManager;

public interface IUploadFileService
{
    Task<ErrorOr<Success>> UploadFileAsync(Stream fileStream, string bucket, string path);
}