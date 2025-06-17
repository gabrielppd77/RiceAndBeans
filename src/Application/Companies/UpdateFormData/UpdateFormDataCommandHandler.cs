using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace Application.Companies.UpdateFormData;

public class UpdateFormDataCommandHandler(
    IUserAuthenticated userAuthenticated,
    IUnitOfWork unitOfWork,
    IUpdateFormDataRepository updateFormDataRepository)
    : IRequestHandler<UpdateFormDataCommand, ErrorOr<Unit>>
{
    public async Task<ErrorOr<Unit>> Handle(UpdateFormDataCommand request, CancellationToken cancellationToken)
    {
        var userId = userAuthenticated.GetUserId();

        var company = await updateFormDataRepository.GetCompanyByUser(userId);

        if (company is null) return Errors.Company.CompanyNotFound;

        company.UpdateFormFields(request.Name, request.Description, request.Path);

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}