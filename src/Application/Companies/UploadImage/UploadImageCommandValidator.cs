using FluentValidation;

namespace Application.Companies.UploadImage;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(x => x.File).NotNull();
    }
}
