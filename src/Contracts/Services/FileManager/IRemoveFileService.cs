using ErrorOr;

namespace Contracts.Services.FileManager;

public interface IRemoveFileService
{
    Task<ErrorOr<Success>> RemoveFileAsync(string bucket, string path);
}