using Domain.Categories;

namespace Contracts.Repositories;

public interface ICategoryRepository
{
    Task Add(Category category);
    Task<IEnumerable<Category>> GetAllByCompanyIdUntracked(Guid companyId);
    Task<Category?> GetById(Guid categoryId);
    void Remove(Category category);
    Task<int> GetLastPositionByCompanyIdUntracked(Guid companyId);
    Task<IEnumerable<Category>> GetAllByIds(IEnumerable<Guid> categoriesId);
}