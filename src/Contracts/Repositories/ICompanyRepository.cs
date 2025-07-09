using Domain.Companies;

namespace Contracts.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetById(Guid userId);
    Task<Company?> GetByIdUntracked(Guid companyId);
}