using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface ICompanyRepository
{
    Task<Company?> GetByUserId(Guid userId);
    Task<Company?> GetByUserIdUntracked(Guid userId);
}