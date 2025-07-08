using ErrorOr;

namespace Application.Project.ApplyMigration;

public interface IApplyMigrationService
{
    Task<ErrorOr<Success>> Handle(ApplyMigrationRequest request);
}