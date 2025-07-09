using Application.Common.Services;
using Contracts.Services.Project.ApplyMigration;
using Contracts.Works;
using Domain.Common.Errors;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace Application.Project.ApplyMigration;

public class ApplyMigrationService(
    IMigrateWork migrateWork,
    ILogger<ApplyMigrationService> logger,
    IApplyMigrationSettingsWrapper applyMigrationSettingsWrapper)
    : IServiceHandler<ApplyMigrationRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(ApplyMigrationRequest request)
    {
        if (applyMigrationSettingsWrapper.MigrationToken != request.Token)
        {
            return Errors.Project.InvalidCredentials;
        }

        await migrateWork.MigrateAsync();

        logger.LogInformation("Migrations successfully applied.");

        return Result.Success;
    }
}