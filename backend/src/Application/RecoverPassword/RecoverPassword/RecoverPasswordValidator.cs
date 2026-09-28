using FluentValidation;

namespace Application.RecoverPassword.RecoverPassword;

public class RecoverPasswordValidator : AbstractValidator<RecoverPasswordRequest>
{
    public RecoverPasswordValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}