using FluentValidation;

namespace Application.Categories.CreateCategory;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}