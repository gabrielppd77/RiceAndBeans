using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface IGetFormDataCompanyRepository
{
    Task<Company?> GetCompanyByUser(Guid userId);
}