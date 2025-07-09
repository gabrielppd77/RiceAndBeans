using ErrorOr;

namespace Contracts.Services.FileManager;

public interface IUploadFileService
{
    Task<ErrorOr<string>> UploadFileAsync(Stream fileStream, string fileName);
}