using FluentValidation;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Token).NotNull();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}
