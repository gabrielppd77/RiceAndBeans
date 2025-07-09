using FluentValidation;

namespace Application.Companies.UpdateFormData;

public class UpdateFormDataValidator : AbstractValidator<UpdateFormDataRequest>
{
    public UpdateFormDataValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Description);
        RuleFor(x => x.Path).NotNull().NotEmpty();
    }
}