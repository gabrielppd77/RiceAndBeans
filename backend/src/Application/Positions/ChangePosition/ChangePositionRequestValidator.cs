using FluentValidation;

namespace Application.Positions.ChangePosition;

public class ChangePositionRequestValidator : AbstractValidator<ChangePositionRequest>
{
    public ChangePositionRequestValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.NewPosition).GreaterThan(0);
    }
}