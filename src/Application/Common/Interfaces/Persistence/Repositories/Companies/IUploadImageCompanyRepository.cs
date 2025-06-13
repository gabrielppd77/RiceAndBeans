using Domain.Companies;

namespace Application.Common.Interfaces.Persistence.Repositories.Companies;

public interface IUploadImageCompanyRepository
{
    Task<Company?> GetCompanyByUser(Guid userId);
}