using Application.Common.Interfaces.Database;
using Application.Common.Interfaces.Persistence.Repositories.Categories;
using Domain.Categories;

namespace Infrastructure.Persistence.Repositories.Categories;

public class CategoryRepository(IApplicationDbContext context) : ICategoryRepository
{
    public async Task Add(Category category)
    {
        await context.Categories.AddAsync(category);
    }
}