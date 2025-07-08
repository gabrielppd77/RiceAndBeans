using FluentValidation;

namespace Application.Users.UploadImage;

public class UploadImageValidator : AbstractValidator<UploadImageRequest>
{
    public UploadImageValidator()
    {
        RuleFor(x => x.File).NotNull();
    }
}