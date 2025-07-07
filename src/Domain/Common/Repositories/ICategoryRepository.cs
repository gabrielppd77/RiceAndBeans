using Domain.Categories;

namespace Domain.Common.Repositories;

public interface ICategoryRepository
{
    Task Add(Category category);
    Task<IEnumerable<Category>> GetAllByCompanyIdUntracked(Guid companyId);
}