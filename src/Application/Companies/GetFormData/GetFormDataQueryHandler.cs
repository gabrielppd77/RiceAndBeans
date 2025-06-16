using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using ErrorOr;
using MediatR;

namespace Application.Companies.GetFormData;

public class GetFormDataQueryHandler :
    IRequestHandler<GetFormDataQuery, ErrorOr<FormDataResult>>
{
    private readonly IFormDataCompanyRepository _formDataCompanyRepository;
    private readonly IUserAuthenticated _userAuthenticated;

    public GetFormDataQueryHandler(
        IFormDataCompanyRepository formDataCompanyRepository,
        IUserAuthenticated userAuthenticated)
    {
        _formDataCompanyRepository = formDataCompanyRepository;
        _userAuthenticated = userAuthenticated;
    }

    public async Task<ErrorOr<FormDataResult>> Handle(GetFormDataQuery request, CancellationToken cancellationToken)
    {
        var userId = _userAuthenticated.GetUserId();

        var company = await _formDataCompanyRepository.GetCompanyByUser(userId);

        return new FormDataResult(company.Name, company.Description, company.Path, company.UrlImage);
    }
}