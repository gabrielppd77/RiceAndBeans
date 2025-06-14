using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence.Repositories.Companies;
using ErrorOr;
using MediatR;

namespace Application.Companies.FormData;

public class FormDataQueryHandler :
    IRequestHandler<FormDataQuery, ErrorOr<FormDataResult>>
{
    private readonly IFormDataCompanyRepository _formDataCompanyRepository;
    private readonly IUserAuthenticated _userAuthenticated;

    public FormDataQueryHandler(
        IFormDataCompanyRepository formDataCompanyRepository,
        IUserAuthenticated userAuthenticated)
    {
        _formDataCompanyRepository = formDataCompanyRepository;
        _userAuthenticated = userAuthenticated;
    }

    public async Task<ErrorOr<FormDataResult>> Handle(FormDataQuery request, CancellationToken cancellationToken)
    {
        var userId = _userAuthenticated.GetUserId();

        var company = await _formDataCompanyRepository.GetCompanyByUser(userId);

        return new FormDataResult(company.Name, company.Path, company.UrlImage);
    }
}