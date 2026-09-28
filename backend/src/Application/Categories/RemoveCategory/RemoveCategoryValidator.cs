using FluentValidation;

namespace Application.Categories.RemoveCategory;

public class RemoveCategoryValidator : AbstractValidator<RemoveCategoryRequest>
{
    public RemoveCategoryValidator()
    {
        RuleFor(x => x.CategoryId).NotEqual(Guid.Empty);
    }
}