using ErrorOr;

namespace Application.Common.Interfaces.FileManager;

public interface IUploadFileService
{
    Task<ErrorOr<string>> UploadFileAsync(Stream fileStream, string fileName);
}