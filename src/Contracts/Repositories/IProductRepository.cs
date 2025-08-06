using Domain.Products;

namespace Contracts.Repositories;

public interface IProductRepository
{
    Task<int> GetLastPositionByCompanyIdUntracked(Guid companyId);
    Task Add(Product product);
    Task<List<Product>> GetAllByCompanyIdUntracked(Guid companyId);
    Task<List<Product>> GetAllWithCategoryRequiredByCompanyIdUntracked(Guid companyId);
    Task<Product?> GetById(Guid productId);
    void Remove(Product product);
    Task<Product?> GetByIdUntracked(Guid productId);
}