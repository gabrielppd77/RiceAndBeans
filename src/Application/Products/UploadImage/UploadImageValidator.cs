using FluentValidation;

namespace Application.Products.UploadImage;

public class UploadImageValidator : AbstractValidator<UploadImageRequest>
{
    public UploadImageValidator()
    {
        RuleFor(x => x.ProductId).NotEqual(Guid.Empty);
        RuleFor(x => x.File).NotNull();
    }
}