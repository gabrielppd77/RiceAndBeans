using Contracts.Services.FileManager;
using Domain.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileManager;

public class RemoveFileService(IMinioClient minioClient, IOptions<FileManagerSettings> fileManagerSettingsOptions)
    : IRemoveFileService
{
    private readonly string _mainBucket = fileManagerSettingsOptions.Value.MainBucket;

    public async Task<ErrorOr<Success>> RemoveFileAsync(string fileName)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(_mainBucket);

        var found = await minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!found) return Errors.FileManager.BucketNotFound;

        var objectArgs = new RemoveObjectArgs()
            .WithBucket(_mainBucket)
            .WithObject(fileName);

        await minioClient.RemoveObjectAsync(objectArgs);

        return Result.Success;
    }
}