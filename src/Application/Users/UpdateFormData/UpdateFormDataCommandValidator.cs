using FluentValidation;

namespace Application.Users.UpdateFormData;

public class UpdateFormDataCommandValidator : AbstractValidator<UpdateFormDataCommand>
{
    public UpdateFormDataCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}