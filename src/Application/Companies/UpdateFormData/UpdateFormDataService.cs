using Contracts.Services.Authentication;
using Application.Common.Services;
using Domain.Common.Errors;
using Contracts.Repositories;
using ErrorOr;

namespace Application.Companies.UpdateFormData;

public class UpdateFormDataService(
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    ICompanyRepository companyRepository)
    : IServiceHandler<UpdateFormDataRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handler(UpdateFormDataRequest request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetById(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        company.UpdateFormFields(request.Name, request.Description, request.Path);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}