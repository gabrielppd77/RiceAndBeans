using FluentValidation;

namespace Application.Products.RemoveProduct;

public class RemoveProductValidator : AbstractValidator<RemoveProductRequest>
{
    public RemoveProductValidator()
    {
        RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
    }
}