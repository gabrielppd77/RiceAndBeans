using FluentValidation;

namespace Application.Project.ApplyMigration;

public class ApplyMigrationCommandValidator : AbstractValidator<ApplyMigrationCommand>
{
    public ApplyMigrationCommandValidator()
    {
        RuleFor(x => x.Token).NotNull().NotEmpty();
    }
}