using FluentValidation;

namespace Application.Users.UpdateFormData;

public class UpdateFormDataValidator : AbstractValidator<UpdateFormDataRequest>
{
    public UpdateFormDataValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}