using Domain.Categories;

namespace Contracts.Repositories;

public interface ICategoryRepository
{
    Task Add(Category category);
    Task<IEnumerable<Category>> GetAllByCompanyIdUntracked(Guid companyId);
}