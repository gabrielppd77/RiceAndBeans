using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.Companies.GetFormData;

public class GetFormDataService(
    ICompanyRepository companyRepository,
    IUserAuthenticated userAuthenticated)
    : IGetFormDataService
{
    public async Task<ErrorOr<FormDataResponse>> Handle()
    {
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetByIdUntracked(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        return new FormDataResponse(company.Name, company.Description, company.Path, company.UrlImage);
    }
}