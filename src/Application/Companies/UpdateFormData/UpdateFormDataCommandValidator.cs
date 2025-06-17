using FluentValidation;

namespace Application.Companies.UpdateFormData;

public class UpdateFormDataCommandValidator : AbstractValidator<UpdateFormDataCommand>
{
    public UpdateFormDataCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Description);
        RuleFor(x => x.Path).NotNull().NotEmpty();
    }
}