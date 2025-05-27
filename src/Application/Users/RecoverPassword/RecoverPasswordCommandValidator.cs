using FluentValidation;

namespace Application.Users.RecoverPassword;

public class RecoverPasswordCommandValidator : AbstractValidator<RecoverPasswordCommand>
{
    public RecoverPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
