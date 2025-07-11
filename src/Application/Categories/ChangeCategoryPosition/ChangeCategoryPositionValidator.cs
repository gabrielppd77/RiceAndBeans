using FluentValidation;

namespace Application.Categories.ChangeCategoryPosition;

public class ChangeCategoryPositionValidator : AbstractValidator<IEnumerable<ChangeCategoryPositionRequest>>
{
    public ChangeCategoryPositionValidator()
    {
        RuleForEach(list => list).SetValidator(new ChangeCategoryPositionRequestValidator());
        RuleFor(list => list)
            .Must(list => list.Any()).WithMessage("Categories must be informed.")
            .Must(list => list.Select(x => x.NewPosition).Distinct().Count() == list.Count()).WithMessage("Positions must be unique.")
            .Must(list => list.All(x => x.NewPosition >= 1 && x.NewPosition <= list.Count())).WithMessage(list => $"NewPosition must be between 1 and {list.Count()}");
    }
}