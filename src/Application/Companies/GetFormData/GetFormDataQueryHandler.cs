using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Companies.GetFormData;

public class GetFormDataQueryHandler(
    ICompanyRepository companyRepository,
    IUserAuthenticated userAuthenticated)
    :
        IRequestHandler<GetFormDataQuery, ErrorOr<FormDataResult>>
{
    public async Task<ErrorOr<FormDataResult>> Handle(GetFormDataQuery request, CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var company = await companyRepository.GetByUserId(userId);

        if (company is null) return Errors.Company.CompanyNotFound;

        return new FormDataResult(company.Name, company.Description, company.Path, company.UrlImage);
    }
}