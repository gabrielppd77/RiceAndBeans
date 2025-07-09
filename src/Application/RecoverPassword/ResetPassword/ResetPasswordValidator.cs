using FluentValidation;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Token).NotNull();
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}