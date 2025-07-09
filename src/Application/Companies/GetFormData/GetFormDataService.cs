using Contracts.Services.Authentication;
using Application.Common.Services;
using Domain.Common.Errors;
using Contracts.Repositories;
using ErrorOr;

namespace Application.Companies.GetFormData;

public class GetFormDataService(
    ICompanyRepository companyRepository,
    IUserAuthenticated userAuthenticated)
    : IServiceHandler<Unit, ErrorOr<FormDataResponse>>
{
    public async Task<ErrorOr<FormDataResponse>> Handler(Unit _)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetByIdUntracked(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        return new FormDataResponse(company.Name, company.Description, company.Path, company.UrlImage);
    }
}