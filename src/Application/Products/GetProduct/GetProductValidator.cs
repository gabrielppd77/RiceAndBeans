using FluentValidation;

namespace Application.Products.GetProduct;

public class GetProductValidator : AbstractValidator<GetProductRequest>
{
    public GetProductValidator()
    {
        RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
    }
}