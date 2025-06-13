using System.Net;
using Application.Common.Interfaces.FileService;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileService;

public class UploadFileService : IUploadFileService
{
    private readonly IMinioClient _minioClient;
    private readonly string _mainBucket;

    public UploadFileService(IMinioClient minioClient, IOptions<UploadFileSettings> uploadFileSettingsOptions)
    {
        _minioClient = minioClient;
        _mainBucket = uploadFileSettingsOptions.Value.MainBucket;
    }

    public async Task<string?> UploadFileAsync(Stream fileStream, string fileName)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(_mainBucket);

        var found = await _minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!found) return null;

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_mainBucket)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length);

        var response = await _minioClient.PutObjectAsync(putObjectArgs);

        if (response.ResponseStatusCode is not HttpStatusCode.OK) return null;

        return $"{_minioClient.Config.Endpoint}/{_mainBucket}/{fileName}";
    }
}