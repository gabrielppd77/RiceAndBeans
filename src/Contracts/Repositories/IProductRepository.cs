using Domain.Products;

namespace Contracts.Repositories;

public interface IProductRepository
{
    Task<int> GetLastPositionByCompanyIdUntracked(Guid companyId);
    Task Add(Product product);
}