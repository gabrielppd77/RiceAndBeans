using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Common.Repositories;
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
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetByIdUntracked(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        return new FormDataResult(company.Name, company.Description, company.Path, company.UrlImage);
    }
}