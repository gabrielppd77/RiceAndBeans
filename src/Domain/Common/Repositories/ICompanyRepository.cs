using Domain.Companies;

namespace Domain.Common.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetById(Guid userId);
    Task<Company?> GetByIdUntracked(Guid companyId);
}