using Application.Common.Interfaces.Project.ApplyMigration;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;
using Microsoft.Extensions.Logging;

namespace Application.Project.ApplyMigration;

public class ApplyMigrationService(
    IUnitOfWork unitOfWork,
    ILogger<ApplyMigrationService> logger,
    IApplyMigrationSettingsWrapper applyMigrationSettingsWrapper) : IApplyMigrationService
{
    public async Task<ErrorOr<Success>> Handle(ApplyMigrationRequest request)
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