using FluentValidation;

namespace Application.RecoverPassword.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Token).NotEqual(Guid.Empty);
        RuleFor(x => x.NewPassword).NotEmpty();
    }
}