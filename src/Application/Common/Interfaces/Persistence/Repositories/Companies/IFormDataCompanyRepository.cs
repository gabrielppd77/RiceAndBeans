using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface IFormDataCompanyRepository
{
    Task<Company?> GetCompanyByUser(Guid userId);
}