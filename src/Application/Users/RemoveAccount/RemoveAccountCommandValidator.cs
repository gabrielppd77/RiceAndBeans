using FluentValidation;

namespace Application.Users.RemoveAccount;

public class RemoveAccountCommandValidator : AbstractValidator<RemoveAccountCommand>
{
	public RemoveAccountCommandValidator()
	{
		RuleFor(x => x.Password).NotEmpty();
	}
}