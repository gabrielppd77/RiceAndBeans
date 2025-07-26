using Application.Common.ServiceHandler;
using Application.Picturies.GetPicture;
using Contracts.Repositories;
using Contracts.Services.Authentication;
using Contracts.Services.FileManager;
using Domain.Common.Errors;
using Domain.Companies;
using ErrorOr;

namespace Application.Companies.GetFormData;

public class GetFormDataService(
    IUserAuthenticated userAuthenticated,
    ICompanyRepository companyRepository,
    IGetPictureService getPictureService,
    IFileManagerSettings fileManagerSettings)
    : IServiceHandler<Unit, ErrorOr<FormDataResponse>>
{
    public async Task<ErrorOr<FormDataResponse>> Handler(Unit _)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetByIdUntracked(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        var picture = await getPictureService.Handler(new GetPictureRequest(nameof(Company), companyId));

        return new FormDataResponse(company.Name, company.Description, company.Path, picture?.GetUrl(fileManagerSettings.BaseUrl));
    }
}