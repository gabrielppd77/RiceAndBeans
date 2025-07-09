using Contracts.Services.Project.ApplyMigration;
using Application.Common.Services;
using Domain.Common.Errors;
using Contracts.Repositories;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace Application.Project.ApplyMigration;

public class ApplyMigrationService(
    IUnitOfWork unitOfWork,
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

        await unitOfWork.MigrateAsync();

        logger.LogInformation("Migrations successfully applied.");

        return Result.Success;
    }
}