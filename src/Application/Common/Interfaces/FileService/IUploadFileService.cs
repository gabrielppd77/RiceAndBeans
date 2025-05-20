namespace Application.Common.Interfaces.FileService;

public interface IUploadFileService
{
    Task<string?> UploadFileAsync(Stream fileStream, string fileName, string bucketName);
}
