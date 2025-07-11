using FluentValidation;

namespace Application.Categories.ChangeCategoryPosition;

public class ChangeCategoryPositionRequestValidator : AbstractValidator<ChangeCategoryPositionRequest>
{
    public ChangeCategoryPositionRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.NewPosition).GreaterThan(0);
    }
}