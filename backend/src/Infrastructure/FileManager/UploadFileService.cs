using System.Net;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using ErrorOr;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileManager;

public class UploadFileService(IMinioClient minioClient) : IUploadFileService
{
    public async Task<ErrorOr<Success>> UploadFileAsync(Stream fileStream, string bucket, string path)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucket);

        var found = await minioClient.BucketExistsAsync(bucketExistsArgs);

        if (!found) return Errors.FileManager.BucketNotFound;

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(path)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length);

        var response = await minioClient.PutObjectAsync(putObjectArgs);

        if (response.ResponseStatusCode is not HttpStatusCode.OK) return Errors.FileManager.UnexpectedError;

        return Result.Success;
    }
}