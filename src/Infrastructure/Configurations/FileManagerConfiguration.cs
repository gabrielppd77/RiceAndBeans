﻿using Contracts.Services.FileManager;
using Infrastructure.FileManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Infrastructure.Configurations;

public static class FileManagerConfiguration
{
    internal static IServiceCollection AddFileManager(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var fileManagerSettings = configuration
            .GetRequiredSection(FileManagerSettings.SectionName)
            .Get<FileManagerSettings>()!;

        services.AddScoped<IUploadFileService>(provider =>
            new UploadFileService(provider.GetRequiredService<IMinioClient>(), fileManagerSettings));
        services.AddScoped<IRemoveFileService>(provider =>
            new RemoveFileService(provider.GetRequiredService<IMinioClient>(), fileManagerSettings));

        return services;
    }
}