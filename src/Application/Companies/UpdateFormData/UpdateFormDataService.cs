using Application.Common.Interfaces.Authentication;
using Domain.Common.Errors;
using Domain.Common.Repositories;
using ErrorOr;

namespace Application.Companies.UpdateFormData;

public class UpdateFormDataService(
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    ICompanyRepository companyRepository)
    : IUpdateFormDataService
{
    public async Task<ErrorOr<Success>> Handle(UpdateFormDataRequest request)
    {
        var companyId = userAuthenticated.GetCompanyId();

        var company = await companyRepository.GetById(companyId);

        if (company is null) return Errors.Company.CompanyNotFound;

        company.UpdateFormFields(request.Name, request.Description, request.Path);

        await unitOfWork.SaveChangesAsync();

        return Result.Success;
    }
}