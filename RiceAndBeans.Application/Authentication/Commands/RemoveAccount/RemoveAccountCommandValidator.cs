using FluentValidation;

namespace RiceAndBeans.Application.Authentication.Commands.RemoveAccount;

public class RemoveAccountCommandValidator : AbstractValidator<RemoveAccountCommand>
{
	public RemoveAccountCommandValidator()
	{
		RuleFor(x => x.Password).NotEmpty();
	}
}