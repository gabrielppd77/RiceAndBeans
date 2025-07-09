using FluentValidation;

namespace Application.Project.ApplyMigration;

public class ApplyMigrationValidator : AbstractValidator<ApplyMigrationRequest>
{
    public ApplyMigrationValidator()
    {
        RuleFor(x => x.Token).NotNull().NotEmpty();
    }
}