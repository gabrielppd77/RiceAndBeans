using FluentValidation;

namespace Application.Positions.ChangePosition;

public class ChangePositionValidator : AbstractValidator<IEnumerable<ChangePositionRequest>>
{
    public ChangePositionValidator()
    {
        RuleForEach(list => list).SetValidator(new ChangePositionRequestValidator());
        RuleFor(list => list)
            .Must(list => list.Any()).WithMessage("List of entities must be informed.")
            .Must(list => list.Select(x => x.NewPosition).Distinct().Count() == list.Count()).WithMessage("Positions must be unique.")
            .Must(list => list.All(x => x.NewPosition >= 1 && x.NewPosition <= list.Count())).WithMessage(list => $"NewPosition must be between 1 and {list.Count()}");
    }
}