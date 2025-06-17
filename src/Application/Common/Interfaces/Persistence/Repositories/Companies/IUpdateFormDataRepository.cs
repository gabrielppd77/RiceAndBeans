using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface IUpdateFormDataRepository
{
    Task<Company?> GetCompanyByUser(Guid userId);
}