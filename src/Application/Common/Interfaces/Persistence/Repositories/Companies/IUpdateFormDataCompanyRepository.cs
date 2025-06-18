using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface IUpdateFormDataCompanyRepository
{
    Task<Company?> GetCompanyByUser(Guid userId);
}