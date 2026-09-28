using FluentValidation;

namespace Application.Users.RemoveAccount;

public class RemoveAccountValidator : AbstractValidator<RemoveAccountRequest>
{
    public RemoveAccountValidator()
    {
        RuleFor(x => x.Password).NotEmpty();
    }
}