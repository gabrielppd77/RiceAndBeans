using Domain.Categories;

namespace Application.Common.Interfaces.Persistence.Repositories.Categories;

public interface ICategoryRepository
{
    Task Add(Category category);
}