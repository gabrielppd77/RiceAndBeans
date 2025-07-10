using System.Net;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using ErrorOr;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileManager;

public class UploadFileService(IMinioClient minioClient, FileManagerSettings fileManagerSettings)
    : IUploadFileService
{
    private readonly string _mainBucket = fileManagerSettings.MainBucket;
    private readonly string _baseUrl = fileManagerSettings.BaseUrl;

    public async Task<ErrorOr<string>> UploadFileAsync(Stream fileStream, string fileName)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(_mainBucket);

        var found = await minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!found) return Errors.FileManager.BucketNotFound;

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_mainBucket)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length);

        var response = await minioClient.PutObjectAsync(putObjectArgs);

        if (response.ResponseStatusCode is not HttpStatusCode.OK) return Errors.FileManager.UnexpectedError;

        return $"{_baseUrl}/{_mainBucket}/{fileName}";
    }
}