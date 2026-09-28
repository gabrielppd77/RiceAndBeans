using FluentValidation;

namespace Application.Products.CreateProduct;

public class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.CategoryId);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description);
        RuleFor(x => x.Price);
    }
}