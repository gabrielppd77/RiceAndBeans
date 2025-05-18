using Microsoft.Extensions.Options;
using System.Net;

using RiceAndBeans.Application.Common.Interfaces.FileService;
using Minio;
using Microsoft.AspNetCore.Http;
using Minio.DataModel.Args;

namespace RiceAndBeans.Infrastructure.FileService;

public class UploadFileService : IUploadFileService
{
    //private readonly IAmazonS3 _s3Client;

    //private readonly IOptions<UploadFileSettings> _uploadFileSettings;

    //public UploadFileService(IOptions<UploadFileSettings> uploadFileSettings)
    //{

    //    _uploadFileSettings = uploadFileSettings;
    //    //var config = new AmazonS3Config
    //    //{
    //    //    ForcePathStyle = true,
    //    //    RegionEndpoint = RegionEndpoint.USEast1,
    //    //};

    //    //config.ServiceURL = uploadFileSettings.Value.ServiceURL;

    //    //_s3Client = new AmazonS3Client(uploadFileSettings.Value.AccessKey, uploadFileSettings.Value.SecretKey, config);
    //}

    //public async Task<string?> UploadFileAsync(Stream fileStream, string bucketName, string fileName, string contentType)
    //{
    //    var config = new AmazonS3Config
    //    {
    //        ForcePathStyle = true,
    //        RegionEndpoint = RegionEndpoint.USEast1,
    //    };

    //    config.ServiceURL = _uploadFileSettings.Value.ServiceURL;

    //    var _s3Client = new AmazonS3Client(_uploadFileSettings.Value.AccessKey, _uploadFileSettings.Value.SecretKey, config);

    //    var putRequest = new PutObjectRequest
    //    {
    //        BucketName = bucketName,
    //        Key = fileName,
    //        InputStream = fileStream,
    //        ContentType = contentType,
    //        CannedACL = S3CannedACL.PublicRead,
    //    };

    //    var response = await _s3Client.PutObjectAsync(putRequest);

    //    if (response.HttpStatusCode is not HttpStatusCode.OK) return null;

    //    return $"{_s3Client.Config.ServiceURL}/{bucketName}/{fileName}";

    //    //var uploadRequest = new TransferUtilityUploadRequest
    //    //{
    //    //    InputStream = fileStream,
    //    //    Key = fileName,
    //    //    BucketName = bucketName,
    //    //    ContentType = contentType,
    //    //    CannedACL = S3CannedACL.PublicRead 
    //    //};

    //    //var fileTransferUtility = new TransferUtility(_s3Client);
    //    //await fileTransferUtility.UploadAsync(uploadRequest);

    //    //return "";
    //}

    private readonly IMinioClient _minioClient;

    public UploadFileService(IOptions<UploadFileSettings> uploadFileSettings)
    {
        _minioClient = new MinioClient()
            .WithEndpoint(uploadFileSettings.Value.ServiceURL)
            .WithCredentials(uploadFileSettings.Value.AccessKey, uploadFileSettings.Value.SecretKey)
            .WithSSL(true)
            .Build();
    }

    public async Task<string?> UploadFileAsync(Stream fileStream, string bucketName, string fileName, string contentType)
    {
        var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
        bool found = await _minioClient.BucketExistsAsync(bucketExistsArgs);
        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
        }

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

        var response = await _minioClient.PutObjectAsync(putObjectArgs);

        if (response.ResponseStatusCode is not HttpStatusCode.OK) return null;

        return $"{_minioClient.Config.BaseUrl}/{bucketName}/{fileName}";
    }
}
