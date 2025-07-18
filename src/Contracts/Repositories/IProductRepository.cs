using Domain.Products;

namespace Contracts.Repositories;

public interface IProductRepository
{
    Task<int> GetLastPositionByCompanyIdUntracked(Guid companyId);
    Task Add(Product product);
    Task<IEnumerable<Product>> GetAllByCompanyIdUntracked(Guid companyId);
    Task<Product> GetById(Guid productId);
    void Remove(Product product);
}