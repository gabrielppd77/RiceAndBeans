using Microsoft.Extensions.Options;
using System.Net;
using Minio;
using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;
using Application.Common.Interfaces.FileService;

namespace Infrastructure.FileService;

public class UploadFileService : IUploadFileService
{
    private readonly IMinioClient _minioClient;

    public UploadFileService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string?> UploadFileAsync(Stream fileStream, string fileName, string bucketName)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);

        var found = await _minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!found) return null;

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length);

        var response = await _minioClient.PutObjectAsync(putObjectArgs);

        if (response.ResponseStatusCode is not HttpStatusCode.OK) return null;

        return $"{_minioClient.Config.Endpoint}/{bucketName}/{fileName}";
    }
}
