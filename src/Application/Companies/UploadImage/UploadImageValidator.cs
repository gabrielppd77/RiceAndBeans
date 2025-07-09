using FluentValidation;

namespace Application.Companies.UploadImage;

public class UploadImageValidator : AbstractValidator<UploadImageRequest>
{
    public UploadImageValidator()
    {
        RuleFor(x => x.File).NotNull();
    }
}