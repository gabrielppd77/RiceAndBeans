using Domain.Companies;

namespace Contracts.Repositories;

public interface ICompanyRepository
{
    Task<Company?> GetById(Guid userId);
    Task<Company?> GetByIdUntracked(Guid companyId);
    Task<Company?> GetByPathUntracked(string companyPath);
    Task<List<Company>> GetAllUntracked();
}