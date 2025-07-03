using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface ICompanyRepository
{
    Task<Company?> GetById(Guid userId);
    Task<Company?> GetByIdUntracked(Guid companyId);
}