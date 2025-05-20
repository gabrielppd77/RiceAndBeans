using FluentValidation;

namespace Application.Authentication.Register;

public class RemoveAccountCommandValidator : AbstractValidator<RegisterCommand>
{
	public RemoveAccountCommandValidator()
	{
		RuleFor(x => x.FirstName).NotEmpty();
		RuleFor(x => x.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.Password).NotEmpty();
		RuleFor(x => x.CompanyName).NotEmpty();
	}
}