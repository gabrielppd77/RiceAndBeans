using ErrorOr;

namespace Application.Common.Interfaces.FileManager;

public interface IRemoveFileService
{
    Task<ErrorOr<Success>> RemoveFileAsync(string fileName);
}