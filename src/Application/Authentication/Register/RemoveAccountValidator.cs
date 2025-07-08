using FluentValidation;

namespace Application.Authentication.Register;

public class RemoveAccountValidator : AbstractValidator<RegisterRequest>
{
    public RemoveAccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.CompanyName).NotEmpty();
    }
}