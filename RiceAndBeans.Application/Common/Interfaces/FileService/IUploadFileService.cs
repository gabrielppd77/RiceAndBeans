namespace RiceAndBeans.Application.Common.Interfaces.FileService;

public interface IUploadFileService
{
    Task<string?> UploadFileAsync(Stream fileStream, string bucketName, string fileName, string contentType);
}
