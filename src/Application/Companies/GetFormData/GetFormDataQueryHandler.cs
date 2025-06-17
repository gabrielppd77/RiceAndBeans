using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using ErrorOr;
using MediatR;

namespace Application.Companies.GetFormData;

public class GetFormDataQueryHandler :
    IRequestHandler<GetFormDataQuery, ErrorOr<FormDataResult>>
{
    private readonly IGetFormDataCompanyRepository _getFormDataCompanyRepository;
    private readonly IUserAuthenticated _userAuthenticated;

    public GetFormDataQueryHandler(
        IGetFormDataCompanyRepository formDataCompanyRepository,
        IUserAuthenticated userAuthenticated)
    {
        _getFormDataCompanyRepository = formDataCompanyRepository;
        _userAuthenticated = userAuthenticated;
    }

    public async Task<ErrorOr<FormDataResult>> Handle(GetFormDataQuery request, CancellationToken cancellationToken)
    {
        var userId = _userAuthenticated.GetUserId();

        var company = await _getFormDataCompanyRepository.GetCompanyByUser(userId);

        return new FormDataResult(company.Name, company.Description, company.Path, company.UrlImage);
    }
}