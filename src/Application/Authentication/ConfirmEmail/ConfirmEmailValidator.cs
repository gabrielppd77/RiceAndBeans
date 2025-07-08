using FluentValidation;

namespace Application.Authentication.ConfirmEmail;

public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailValidator()
    {
        RuleFor(x => x.Token).NotNull();
    }
}