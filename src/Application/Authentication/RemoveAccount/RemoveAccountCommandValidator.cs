using FluentValidation;

namespace Application.Authentication.RemoveAccount;

public class RemoveAccountCommandValidator : AbstractValidator<RemoveAccountCommand>
{
	public RemoveAccountCommandValidator()
	{
		RuleFor(x => x.Password).NotEmpty();
	}
}