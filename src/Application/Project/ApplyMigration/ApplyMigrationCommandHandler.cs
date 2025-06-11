using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Project.ApplyMigration;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Project.ApplyMigration;

public class ApplyMigrationCommandHandler : IRequestHandler<ApplyMigrationCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ApplyMigrationCommandHandler> _logger;
    private readonly string _migrationToken;

    public ApplyMigrationCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<ApplyMigrationCommandHandler> logger,
        IApplyMigrationSettingsWrapper _applyMigrationSettingsWrapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _migrationToken = _applyMigrationSettingsWrapper.MigrationToken;
    }

    public async Task<ErrorOr<Unit>> Handle(ApplyMigrationCommand request, CancellationToken cancellationToken)
    {
        if (_migrationToken != request.Token)
        {
            return Errors.Project.InvalidCredentials;
        }

        await _unitOfWork.MigrateAsync();

        _logger.LogInformation("Migrations successfully applied.");

        return Unit.Value;
    }
}