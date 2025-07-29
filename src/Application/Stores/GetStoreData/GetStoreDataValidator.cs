using FluentValidation;

namespace Application.Stores.GetStoreData;

public class GetStoreDataValidator : AbstractValidator<GetStoreDataRequest>
{
    public GetStoreDataValidator()
    {
        RuleFor(x => x.CompanyPath).NotEmpty();
    }
}