using FluentValidation;

namespace Application.RecoverPassword.RecoverPassword;

public class RecoverPasswordCommandValidator : AbstractValidator<RecoverPasswordCommand>
{
    public RecoverPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
