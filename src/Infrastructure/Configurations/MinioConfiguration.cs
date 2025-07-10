using Infrastructure.FileManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Infrastructure.Configurations;

public static class MinioConfiguration
{
    public static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        var uploadFileSettings = configuration
            .GetRequiredSection(FileManagerSettings.SectionName)
            .Get<FileManagerSettings>()!;

        services.AddMinio(configureClient => configureClient
            .WithEndpoint(uploadFileSettings.Host)
            .WithCredentials(uploadFileSettings.AccessKey, uploadFileSettings.SecretKey)
            .WithSSL(uploadFileSettings.EnableSsl)
            .Build());

        return services;
    }
}