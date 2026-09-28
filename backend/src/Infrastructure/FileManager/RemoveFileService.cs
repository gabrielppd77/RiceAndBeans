using Contracts.Services.FileManager;
using Domain.Common.Errors;
using ErrorOr;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileManager;

public class RemoveFileService(IMinioClient minioClient) : IRemoveFileService
{
    public async Task<ErrorOr<Success>> RemoveFileAsync(string bucket, string path)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucket);

        var found = await minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!found) return Errors.FileManager.BucketNotFound;

        var objectArgs = new RemoveObjectArgs()
            .WithBucket(bucket)
            .WithObject(path);

        await minioClient.RemoveObjectAsync(objectArgs);

        return Result.Success;
    }
}